using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FitnessCenter.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ServiceId { get; set; }
        [ForeignKey(nameof(ServiceId))]
        public Service Service { get; set; } = null!;
        [Required]
        public int TrainerId { get; set; }
        [ForeignKey(nameof(TrainerId))]
        public Trainer Trainer { get; set; } = null!;
        [Required]
        [Display(Name ="Appointment Date")]
        public DateTime AppointmentDate { get; set; }
        [Required]
        [Display(Name ="Start Time")]
        public TimeSpan StartTime { get; set; }
        [Required]
        [Display(Name ="End Time")]
        public TimeSpan EndTime { get; set; }
        public double Fee { get; set; }
        [Display(Name ="State")]
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
        [Required]
        public string UserId { get; set; } 

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;
    }
    public enum AppointmentStatus { Pending, Confirmed, Cancelled }
}
