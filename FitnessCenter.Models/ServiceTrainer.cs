using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FitnessCenter.Models
{
    public  class ServiceTrainer
    {
        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; }=null!;
        public int ServiceId { get; set; }
        public Service Service  { get; set; }=null!;
    }
}
