using FitnessCenter.DataAccess.Repository.IRepository;
using FitnessCenter.Models;
using FitnessCenter.Models.ViewModels;
using FitnessCenter.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FitnessCenter.Areas.Admin.Controllers
{
    [Area(StaticDetails.Role_Admin)]
    [Authorize(Roles=StaticDetails.Role_Admin)]
    public class ServiceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ServiceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Service> Services = _unitOfWork.ServiceRepo.GetAll(includeProperties: "ServiceTrainers.Trainer").ToList();
            return View(Services);
        }
        public IActionResult Upsert(int? id)
        {

            ServiceTrainerViewModel vm = new ServiceTrainerViewModel() { 
                Service =new Service(),
                SelectedTrainerIds = new List<int>()
            };
            if (id == null || id == 0)
            {
                //Create
                ViewData["Title"] = "Create Service";
            }
            else
            {
                //Update
                vm.Service = _unitOfWork.ServiceRepo.Get(u => u.Id == id,includeProperties:"ServiceTrainers.Trainer");
                if (vm.Service == null)
                {
                    return NotFound();
                }
                vm.SelectedTrainerIds = vm.Service.ServiceTrainers.Select(st => st.TrainerId).ToList();

                ViewData["Title"] = "Update Service";
            }
            vm.Trainers = _unitOfWork.TrainerRepo.GetAll()
                .Select(t => new SelectListItem
                {
                    Text = t.Name,
                    Value = t.Id.ToString(),
                    Selected = vm.SelectedTrainerIds.Contains(t.Id)
                });

            return View(vm);
        }
        [HttpPost]
        public IActionResult Upsert(ServiceTrainerViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.Service.Id == 0)
                {
                    _unitOfWork.ServiceRepo.Add(vm.Service);
                    TempData["success"] = "Service created successfully";
                }
                else
                {
                    _unitOfWork.ServiceRepo.Update(vm.Service);
                    TempData["success"] = "Service Updated successfully";
                }
                _unitOfWork.Save();
                if (vm.Service.Id != 0)
                {
                    // Hizmete ait mevcut tüm ServiceTrainer kayıtlarını Eager Loading yapmadan çek
                    var oldLinks = _unitOfWork.ServiceTrainerRepo.GetAll(st => st.ServiceId == vm.Service.Id).ToList();

                    // Hepsini sil
                    if (oldLinks.Any())
                    {
                        _unitOfWork.ServiceTrainerRepo.RemoveRange(oldLinks);
                        _unitOfWork.Save();
                    }
                }

                // 3. Yeni İlişkileri Kaydetme
                if (vm.SelectedTrainerIds != null && vm.SelectedTrainerIds.Any())
                {
                    foreach (var trainerId in vm.SelectedTrainerIds)
                    {
                        var newLink = new ServiceTrainer
                        {
                            ServiceId = vm.Service.Id, // Yeni veya güncellenmiş ID
                            TrainerId = trainerId
                        };
                        _unitOfWork.ServiceTrainerRepo.Add(newLink);
                    }
                    _unitOfWork.Save();
                }

                return RedirectToAction("Index");
            }
            else
            {
                vm.Trainers = _unitOfWork.TrainerRepo.GetAll()
                .Select(t => new SelectListItem { Text = t.Name, Value = t.Id.ToString() });
                vm.Services = _unitOfWork.ServiceRepo.GetAll()
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
            Service Service = _unitOfWork.ServiceRepo.Get(obj => obj.Id == id);
            if (Service == null)
            {
                return NotFound();
            }
            return View(Service);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(Service Service)
        {
            _unitOfWork.ServiceRepo.Remove(Service);
            _unitOfWork.Save();
            TempData["success"] = "Service deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
