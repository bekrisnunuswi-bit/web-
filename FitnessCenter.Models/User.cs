using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing.Constraints;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FitnessCenter.Models
{
    public class User : IdentityUser
    {
        [Required]
        [DisplayName("Full Name")]
        [MaxLength(15)]
        public string Name { get; set; } = null!;
        [DisplayName("Date Of Birth")]
        public DateOnly DateOfBirth { get; set; }
        [Range(1,1000)]
        public IEnumerable<Appointment>? Appointments { get; set; }
        public double? Height {  get; set; }
        [Range(1,1000)]
        public double? Weight { get; set; }
    }
}
