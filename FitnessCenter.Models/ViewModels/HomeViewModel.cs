using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace FitnessCenter.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Service> Services { get; set; } = null!;
        public Center CenterDetails { get;  set; } = null!;
    }
}
