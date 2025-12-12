using FitnessCenter.DataAccess.Data;
using FitnessCenter.DataAccess.Repository.IRepository;
using FitnessCenter.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessCenter.DataAccess.Repository
{
    public class ServiceRepository : Repository<Service>, IServiceRepository
    {
        private readonly ApplicationDbContext _dbcontext;
        public ServiceRepository(ApplicationDbContext dbcontext):base(dbcontext) { 
            _dbcontext = dbcontext;
        }
        public void Update(Service obj)
        {
            _dbcontext.Services.Update(obj);
        }
    }
}
