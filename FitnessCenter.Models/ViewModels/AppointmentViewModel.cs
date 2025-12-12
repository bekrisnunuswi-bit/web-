using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using FitnessCenter.Models; 
namespace FitnessCenter.Models.ViewModels
{
    public class AppointmentViewModel
    {
        [Required]
        public Service Service { get; set; } = null!; 
        [Required]
        public int ServiceId { get; set; } 

        [Required(ErrorMessage = "Lütfen bir eğitmen seçiniz.")]
        public int TrainerId { get; set; }
        public IEnumerable<SelectListItem>? TrainerList { get; set; }

        [DataType(DataType.Date)]
        public DateTime? AppointmentDate { get; set; }
        public int DurationMinutes { get; set; } 
        public double Fee { get; set; }
        [Required]
        public TimeSpan StartTime { get; set; }
        public List<TimeSpan>? AvailableSlots { get; set; }
    }
}
