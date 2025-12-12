using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FitnessCenter.Models
{
    public class BodyProfile
    {
        [Key]
        public int Id { get; set; }

        public string? UserId { get; set; }

        public double? HeightCm { get; set; }
        public double? WeightKg { get; set; }

        public string? BodyType { get; set; }

        public string? ImageUrl { get; set; }

        public string? Recommendation { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
