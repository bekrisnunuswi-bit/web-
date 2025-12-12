using FitnessCenter.DataAccess.Repository.IRepository;
using FitnessCenter.Models;
using FitnessCenter.Models.ViewModels;
using FitnessCenter.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FitnessCenter.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetails.Role_Admin)]
    public class TrainerController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public TrainerController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            // After _context u must determine whitch Db set u want to retrieve
            List<Trainer> Trainers = _unitOfWork.TrainerRepo.GetAll(includeProperties: "ServiceTrainers.Service").ToList();
            return View(Trainers);
        }
        public IActionResult Upsert(int? id)
        {
            ServiceTrainerViewModel vm = new ServiceTrainerViewModel() { 
                Trainer = new Trainer(),
                SelectedServiceIds = new List<int>()
            };
            if (id == null || id == 0)
            {
                //Create
                ViewData["Title"] = "Create Trainer";
            }
            else
            {
                //Update
                vm.Trainer = _unitOfWork.TrainerRepo.Get(u => u.Id == id, includeProperties: "ServiceTrainers.Service");
                if (vm.Trainer == null)
                {
                    return NotFound();
                }
                vm.SelectedServiceIds = vm.Trainer.ServiceTrainers.Select(st => st.ServiceId).ToList();
                ViewData["Title"] = "Update Trainer";
            }
            vm.Services = _unitOfWork.ServiceRepo.GetAll()
                .Select(t => new SelectListItem
                {
                    Text = t.Name,
                    Value = t.Id.ToString(),
                    Selected = vm.SelectedServiceIds.Contains(t.Id)
                });

            return View(vm);
        }
        [HttpPost,ActionName("Upsert")]
        public IActionResult Upsert(ServiceTrainerViewModel vm, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string TrainerPath = Path.Combine(wwwRootPath, @"images\Trainer");
                    if (!string.IsNullOrEmpty(vm.Trainer.ProfileImageUrl))
                    {
                        //delete the old image
                        var oldImagePath = Path.Combine(wwwRootPath, vm.Trainer.ProfileImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }

                    }
                    using (var fileStream = new FileStream(Path.Combine(TrainerPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    vm.Trainer.ProfileImageUrl = @"\images\Trainer\" + fileName;
                }
                if (vm.Trainer.Id == 0)
                {
                    _unitOfWork.TrainerRepo.Add(vm.Trainer);
                    TempData["success"] = "Trainer created successfully";
                }
                else
                {
                    _unitOfWork.TrainerRepo.Update(vm.Trainer);
                    TempData["success"] = "Trainer Updated successfully";
                }
                _unitOfWork.Save();
                if (vm.Trainer.Id != 0)
                {
                    var oldLinks = _unitOfWork.ServiceTrainerRepo.GetAll(st => st.TrainerId == vm.Trainer.Id).ToList();
                    if (oldLinks.Any())
                    {
                        _unitOfWork.ServiceTrainerRepo.RemoveRange(oldLinks);
                        _unitOfWork.Save();
                    }
                }

                // 3. Yeni İlişkileri Kaydetme
                if (vm.SelectedServiceIds != null && vm.SelectedServiceIds.Any())
                {
                    foreach (var serviceId in vm.SelectedServiceIds)
                    {
                        var newLink = new ServiceTrainer
                        {
                            TrainerId = vm.Trainer.Id, // Yeni veya güncellenmiş ID
                            ServiceId = serviceId
                        };
                        _unitOfWork.ServiceTrainerRepo.Add(newLink);
                    }
                    _unitOfWork.Save();
                }

                return RedirectToAction("Index");
            }
            else
            {
                vm.Services = _unitOfWork.ServiceRepo.GetAll()
                    .Where(s => s.IsActive)
                    .Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() });
                vm.Trainers = _unitOfWork.TrainerRepo.GetAll()
                     .Select(t => new SelectListItem { Text = t.Name, Value = t.Id.ToString() });

                return View(vm);
            }
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Trainer? Trainer = _unitOfWork.TrainerRepo.Get(obj => obj.Id == id);
            if (Trainer == null)
            {
                return NotFound();
            }
            return View(Trainer);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(Trainer Trainer)
        {
            _unitOfWork.TrainerRepo.Remove(Trainer);
            _unitOfWork.Save();
            TempData["success"] = "Trainer deleted successfully";
            return RedirectToAction("Index");
        }
    }
}