using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FitnessCenter.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [ValidateNever]
        public string? Description { get; set; }
        [Required]
        [DisplayName("Duration Minutes")]
        public int DurationMinutes {  get; set; }
        [Required]
        public double Fee { get; set; }
        [Required]
        [DisplayName("Max Capacity")]
        public int MaxCapacity {  get; set; }
        [ValidateNever]
        [DisplayName("Activity")]
        public bool IsActive { get; set; } = true;
        public ICollection<ServiceTrainer> ServiceTrainers { get; set; }= new List<ServiceTrainer>();
    }
}
