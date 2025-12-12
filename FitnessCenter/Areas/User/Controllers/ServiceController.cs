using FitnessCenter.DataAccess.Repository.IRepository;
using FitnessCenter.Models;
using FitnessCenter.Models.ViewModels;
using FitnessCenter.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessCenter.Areas.User.Controllers
{
    [Area("User")]
    public class ServiceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ServiceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Service> Services = _unitOfWork.ServiceRepo.GetAll(includeProperties:"ServiceTrainers.Trainer");
            return View(Services);
        }
        public IActionResult Details(int id)
        {
            Service service = _unitOfWork.ServiceRepo.Get(u => u.Id == id, includeProperties: "ServiceTrainers.Trainer");
            if (service == null) return NotFound();
            return View(service);
        }
    }
}
