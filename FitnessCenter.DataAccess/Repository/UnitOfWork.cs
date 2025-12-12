using FitnessCenter.DataAccess.Data;
using FitnessCenter.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessCenter.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly ApplicationDbContext _dbcontext; 
        public ITrainerRepository TrainerRepo {  get; private set; }
        public IServiceRepository ServiceRepo { get; private set; }
        public IUserRepository UserRepo { get; private set; }
        public IFitnessCenterRepository FitnessCenterRepo { get; private set; }
        public IServiceTrainerRepository ServiceTrainerRepo { get; private set; }
        public ITrainerAvailabilityRepository TrainerAvailabilityRepo { get; private set; }
        public IAppointmentRepository AppointmentRepo { get; private set; }


        public UnitOfWork(ApplicationDbContext dbcontext)
        {
            _dbcontext= dbcontext;
            TrainerRepo=new TrainerRepository(_dbcontext);
            ServiceRepo=new ServiceRepository(_dbcontext);
            UserRepo=new UserRepository(_dbcontext);
            FitnessCenterRepo = new FitnessCenterRepository(_dbcontext);
            ServiceTrainerRepo = new ServiceTrainerRepository(_dbcontext);
            TrainerAvailabilityRepo = new TrainerAvailabilityRepository(_dbcontext);
            AppointmentRepo = new AppointmentRepository(_dbcontext);

        }
        public void Save()
        {
            _dbcontext.SaveChanges();
        }
    }
}
