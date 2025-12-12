using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessCenter.Models.ViewModels
{
    public class ServiceTrainerViewModel
    {
        public Service? Service {  get; set; }
        public Trainer? Trainer { get; set; }
        public IEnumerable<SelectListItem>? Services { get; set; }
        public IEnumerable<SelectListItem>? Trainers { get; set; }
        public List<int>? SelectedTrainerIds { get; set; }
        public List<int>? SelectedServiceIds { get; set; }
    }
}
