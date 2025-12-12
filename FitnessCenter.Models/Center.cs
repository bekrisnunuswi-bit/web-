using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FitnessCenter.Models
{
    public class Center
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        [Required]
        public TimeSpan OpeningTime { get; set; }
        [Required]
        public TimeSpan ClosingTime { get; set; }
    }
}
