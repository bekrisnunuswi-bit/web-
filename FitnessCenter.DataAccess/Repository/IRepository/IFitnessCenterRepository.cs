using FitnessCenter.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessCenter.DataAccess.Repository.IRepository
{
    public interface IFitnessCenterRepository
    {
        Center Get();
        void Update(Center obj);
    }
}
