using FitnessCenter.DataAccess.Repository.IRepository;
using FitnessCenter.Models;
using FitnessCenter.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FitnessCenter.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            HomeViewModel centerDetails = new HomeViewModel()
            {
                Services = _unitOfWork.ServiceRepo.GetAll(includeProperties: "ServiceTrainers.Trainer").Take(3),
                CenterDetails = _unitOfWork.FitnessCenterRepo.Get()
            };
            return View(centerDetails);
        }
        public IActionResult Details(int id)
        {
            Service service = _unitOfWork.ServiceRepo.Get(u => u.Id == id, includeProperties: "ServiceTrainers.Trainer");
            return View(service);
        }
        public IActionResult AIPLan(int id)
        {
            return View(User);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
