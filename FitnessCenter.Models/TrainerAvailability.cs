using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FitnessCenter.Models
{
    public class TrainerAvailability
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int TrainerId { get; set; } 
        public Trainer Trainer { get; set; } = null!;

        [Required]
        [DisplayName("Day Of Week")]
        public DayOfWeek DayOfWeek { get; set; } 

        [Required]
        [DisplayName("Start Time")]
        public TimeSpan StartTime { get; set; } 

        [Required]
        [DisplayName("End Time")]
        public TimeSpan EndTime { get; set; }
    }
}
