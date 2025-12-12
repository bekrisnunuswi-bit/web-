// FitnessCenter.Services/OpenAIService.cs
using FitnessCenter.Areas.User.Services;
using FitnessCenter.DataAccess.Data;
using FitnessCenter.Models;
using FitnessCenter.Models.ViewModels;
using FitnessCenter.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FitnessCenter.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles =StaticDetails.Role_User)] 
    public class AiController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IAIService _ai;
        private readonly IWebHostEnvironment _env;

        public AiController(ApplicationDbContext db, IAIService ai, IWebHostEnvironment env)
        {
            _db = db;
            _ai = ai;
            _env = env;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new AIViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(AIViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var profile = new BodyProfile
            {
                UserId = User?.Identity?.Name, // or user id from claims depending on your identity setup
                HeightCm = vm.HeightCm,
                WeightKg = vm.WeightKg,
                BodyType = vm.BodyType
            };

            if (vm.UploadedImage != null && vm.UploadedImage.Length > 0)
            {
                var uploads = Path.Combine(_env.WebRootPath, "uploads", "ai");
                Directory.CreateDirectory(uploads);
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(vm.UploadedImage.FileName)}";
                var filePath = Path.Combine(uploads, fileName);
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    await vm.UploadedImage.CopyToAsync(fs);
                }
                // store web-accessible path
                profile.ImageUrl = $"/uploads/ai/{fileName}";
            }

            // call AI
            profile.Recommendation = await _ai.GenerateRecommendationAsync(profile);

            // persist
            _db.BodyProfiles.Add(profile);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Result), new { id = profile.Id });
        }

        [HttpGet]
        public IActionResult Result(int id)
        {
            var profile = _db.BodyProfiles.Find(id);
            if (profile == null) return NotFound();
            return View(profile);
        }
    }
}