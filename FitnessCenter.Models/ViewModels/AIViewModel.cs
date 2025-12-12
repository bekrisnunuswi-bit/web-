using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FitnessCenter.Models.ViewModels
{
    public class AIViewModel
    {
        [Display(Name = "Height (cm)")]
        public double? HeightCm { get; set; }

        [Display(Name = "Weight (kg)")]
        public double? WeightKg { get; set; }

        [Display(Name = "Body Type")]
        public string? BodyType { get; set; }

        [Display(Name = "Upload Photo (optional)")]
        public IFormFile? UploadedImage { get; set; }
    }
}
