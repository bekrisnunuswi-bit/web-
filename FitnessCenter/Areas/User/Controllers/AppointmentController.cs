using FitnessCenter.DataAccess.Repository.IRepository;
using FitnessCenter.Models;
using FitnessCenter.Models.ViewModels;
using FitnessCenter.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FitnessCenter.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = StaticDetails.Role_User)]
    public class AppointmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public AppointmentController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        private void ReCalculateSlots(AppointmentViewModel vm)
        {
            var service = _unitOfWork.ServiceRepo.Get(s => s.Id == vm.ServiceId);
            var centerDetails = _unitOfWork.FitnessCenterRepo.Get();

            vm.Service = service;

            var fullService = _unitOfWork.ServiceRepo.Get(s => s.Id == vm.ServiceId, includeProperties: "ServiceTrainers.Trainer");
            if (fullService != null)
            {
                vm.TrainerList = fullService.ServiceTrainers
                   .Select(st => new SelectListItem { Text = $"{st.Trainer.Name} ({st.Trainer.Specialty})", Value = st.TrainerId.ToString() })
                   .DistinctBy(t => t.Value);
            }

            if (vm.TrainerId != 0 && vm.AppointmentDate.HasValue)
            {
                DayOfWeek day = vm.AppointmentDate.Value.DayOfWeek;
                DateTime appointmentDate = vm.AppointmentDate.Value.Date;

                var generalAvailability = _unitOfWork.TrainerAvailabilityRepo
                    .GetAll(ta => ta.TrainerId == vm.TrainerId && ta.DayOfWeek == day)
                    .ToList();

                var existingAppointments = _unitOfWork.AppointmentRepo
                    .GetAll(a => a.TrainerId == vm.TrainerId &&
                                 a.AppointmentDate.Date == appointmentDate &&
                                 a.Status != AppointmentStatus.Cancelled)
                    .ToList();

                vm.AvailableSlots = TimeManagerService.CalculateAvailableSlots(
                    generalAvailability,
                    existingAppointments,
                    vm.DurationMinutes,
                    centerDetails?.ClosingTime ?? TimeSpan.FromHours(23, 59)
                );
            }
        }
        [HttpGet]
        public IActionResult Create(int id) // serviceId'yi URL'den alır
        {
            var service = _unitOfWork.ServiceRepo.Get(s => s.Id == id, includeProperties: "ServiceTrainers.Trainer");

            if (service == null) return NotFound("Hizmet bulunamadı.");

            AppointmentViewModel vm = new AppointmentViewModel
            {
                Service = service,
                ServiceId = id,
                DurationMinutes = service.DurationMinutes,
                Fee = service.Fee,
                AppointmentDate = DateTime.Now.Date.AddDays(1),

                TrainerList = service.ServiceTrainers
                    .Select(st => new SelectListItem { Text = $"{st.Trainer.Name} ({st.Trainer.Specialty})", Value = st.TrainerId.ToString() })
                    .DistinctBy(t => t.Value)
            };

            return View(vm);
        }
        [HttpPost]
        public IActionResult Create(AppointmentViewModel vm)
        {
            if (vm.TrainerId == 0 || !vm.AppointmentDate.HasValue || vm.ServiceId == 0)
            {
                ReCalculateSlots(vm);
                ModelState.AddModelError(string.Empty, "Lütfen bir eğitmen ve randevu tarihi seçiniz.");
                return View(vm);
            }

            ReCalculateSlots(vm);

            if (vm.AvailableSlots == null || !vm.AvailableSlots.Any())
            {
                ModelState.AddModelError(string.Empty, "Seçtiğiniz gün ve saatte eğitmenin müsait slotu bulunmamaktadır.");
                return View(vm);
            }

            return View("SelectAvailableDate", vm);
        }
        [HttpPost]
        public IActionResult SelectAvailableDate(AppointmentViewModel vm)
        {
            var service = _unitOfWork.ServiceRepo.Get(s => s.Id == vm.ServiceId);
            vm.Service = service;

            if (string.IsNullOrEmpty(vm.AppointmentDate.ToString()) && string.IsNullOrEmpty(vm.StartTime.ToString()))
            {
                ReCalculateSlots(vm); // Hata durumunda slotları tekrar hesapla
                return View("SelectAvailableDate", vm);
            }

            TimeSpan endTime = vm.StartTime.Add(TimeSpan.FromMinutes(vm.DurationMinutes));
            bool isConflicting = _unitOfWork.AppointmentRepo
                .GetAll(a => a.TrainerId == vm.TrainerId &&
                             a.AppointmentDate.Date == vm.AppointmentDate.Value.Date &&
                             (a.StartTime < endTime && a.EndTime > vm.StartTime) &&
                             a.Status != AppointmentStatus.Cancelled)
                .Any();

            if (isConflicting)
            {
                ModelState.AddModelError(string.Empty, "Bu saatte çakışan randevu var. Lütfen geri dönüp başka bir saat seçin.");
                ReCalculateSlots(vm);
                return View("SelectAvailableDate", vm);
            }

            string userId = _userManager.GetUserId(User);

            Appointment appointment = new Appointment
            {
                UserId = userId,
                ServiceId = vm.ServiceId,
                TrainerId = vm.TrainerId,
                AppointmentDate = vm.AppointmentDate.Value.Date,
                StartTime = vm.StartTime,
                EndTime = endTime,
                Fee = vm.Fee,
                Status = AppointmentStatus.Pending
            };

            _unitOfWork.AppointmentRepo.Add(appointment);
            _unitOfWork.Save();

            TempData["success"] = "Randevunuz başarıyla oluşturuldu ve onay bekliyor.";
            return RedirectToAction("ReservedAppointments");
        }
        public IActionResult ReservedAppointments()
        {
            string currentUserId = _userManager.GetUserId(User);

            IEnumerable<Appointment> myAppintments = _unitOfWork.AppointmentRepo
                .GetAll(
                    filter: a => a.UserId == currentUserId,
                    includeProperties: "Service,Trainer"
                );

            return View(myAppintments);
        }
        [HttpGet]
        public IActionResult Cancel(int id)
        {
            string currentUserId = _userManager.GetUserId(User);
            Appointment appointment = _unitOfWork.AppointmentRepo
                .Get(
                    filter: a => a.UserId == currentUserId && a.Id==id,
                    includeProperties: "Service,Trainer"
                );
            if (appointment == null) return NotFound();
            appointment.Status = AppointmentStatus.Cancelled;
            _unitOfWork.AppointmentRepo.Update(appointment);
            _unitOfWork.Save();
            TempData["success"] = "Appointment Cancelled Successfully";
            return RedirectToAction("ReservedAppointments");
        }
    }
}