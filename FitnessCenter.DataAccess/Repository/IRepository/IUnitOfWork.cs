using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessCenter.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ITrainerRepository TrainerRepo { get; }
        IServiceRepository ServiceRepo { get; }
        IUserRepository UserRepo { get; }
        IFitnessCenterRepository FitnessCenterRepo { get; }
        IServiceTrainerRepository ServiceTrainerRepo { get; }
        ITrainerAvailabilityRepository TrainerAvailabilityRepo { get; }
        IAppointmentRepository AppointmentRepo { get; }

        void Save(); 
    }
}
