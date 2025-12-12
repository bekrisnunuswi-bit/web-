using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FitnessCenter.Models
{
    public class Trainer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [ValidateNever]
        public string? Bio {  get; set; }
        [Required]
        public string? Specialty { get; set; }
        [ValidateNever]
        [Display(Name="Profile Image")]
        public string? ProfileImageUrl {  get; set; }
        public ICollection<ServiceTrainer> ServiceTrainers { get; set; } = new List<ServiceTrainer>();
        public ICollection<TrainerAvailability> Availabilities { get; set; } = new List<TrainerAvailability>();

    }
}
