using FitnessCenter.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace FitnessCenter.DataAccess.Repository.IRepository
{
    public interface ITrainerAvailabilityRepository
    {
        TrainerAvailability Get(Expression<Func<TrainerAvailability, bool>>? filter , string? includeProperties = null);
        IEnumerable<TrainerAvailability> GetAll(Expression<Func<TrainerAvailability, bool>>? filter , string? includeProperties = null);
        void Update(TrainerAvailability obj);
    }
}
