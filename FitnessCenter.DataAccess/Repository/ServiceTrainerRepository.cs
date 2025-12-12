using FitnessCenter.DataAccess.Data;
using FitnessCenter.DataAccess.Repository.IRepository;
using FitnessCenter.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace FitnessCenter.DataAccess.Repository
{
    public class ServiceTrainerRepository : Repository<ServiceTrainer>, IServiceTrainerRepository
    {
        private readonly ApplicationDbContext _dbcontext;
        public ServiceTrainerRepository(ApplicationDbContext dbcontext) : base(dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public void Update(ServiceTrainer obj)
        {
            _dbcontext.ServiceTrainers.Update(obj);
        }
    }
}
