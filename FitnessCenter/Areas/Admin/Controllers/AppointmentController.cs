using FitnessCenter.DataAccess.Repository.IRepository;
using FitnessCenter.Models;
using FitnessCenter.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessCenter.Areas.Admin.Controllers
{
    [Area(StaticDetails.Role_Admin)]
    [Authorize(Roles = StaticDetails.Role_Admin)]
    public class AppointmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public AppointmentController(IUnitOfWork unitOfWork)
        {
                _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Appointment> appointments = _unitOfWork.AppointmentRepo
                .GetAll(a => a.Status == AppointmentStatus.Pending, includeProperties: "Service,User,Trainer"); 
            return View(appointments);
        }
        [HttpGet]
        public IActionResult Confirm(int Id)
        {
            Appointment appointment = _unitOfWork.AppointmentRepo.Get(obj => obj.Id == Id);
            if (appointment == null) return NotFound();
            appointment.Status = AppointmentStatus.Confirmed;
            _unitOfWork.AppointmentRepo.Update(appointment);
            _unitOfWork.Save();
            TempData["success"] = "Appointment Confirmed Successfully";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Cancel(int Id)
        {
            Appointment appointment = _unitOfWork.AppointmentRepo.Get(obj => obj.Id == Id);
            if(appointment==null) return NotFound();
            appointment.Status = AppointmentStatus.Cancelled;
            _unitOfWork.AppointmentRepo.Update(appointment);
            _unitOfWork.Save();
            TempData["success"] = "Appointment Cancelled Successfully";
            return RedirectToAction("Index");
        }
    }
}
