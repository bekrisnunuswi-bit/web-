using FitnessCenter.DataAccess.Repository.IRepository;
using FitnessCenter.Models;
using FitnessCenter.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessCenter.Areas.Admin.Controllers
{
    [Area(StaticDetails.Role_Admin)]
    [Authorize(Roles = StaticDetails.Role_Admin)]
    public class CenterController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CenterController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            Center center = _unitOfWork.FitnessCenterRepo.Get();
            return View(center);
        }
        public IActionResult Edit()
        {
            Center center = _unitOfWork.FitnessCenterRepo.Get();
            return View(center);
        }
        [HttpPost]
        public IActionResult Edit(Center obj)
        {
            _unitOfWork.FitnessCenterRepo.Update(obj);
            return RedirectToAction("Index");
        }
    }
}
