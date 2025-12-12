using FitnessCenter.DataAccess.Data;
using FitnessCenter.DataAccess.Repository.IRepository;
using FitnessCenter.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace FitnessCenter.DataAccess.Repository
{
    public class FitnessCenterRepository : IFitnessCenterRepository
    {
        private readonly ApplicationDbContext _dbcontext;
        public FitnessCenterRepository(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext; 
        }
        public Models.Center Get()
        {
            Models.Center query = _dbcontext.FitnessCenter.FirstOrDefault();
            return query;
        }
        public void Update(Models.Center obj)
        {
            var centerFromDb = _dbcontext.FitnessCenter.FirstOrDefault(c => c.Id == obj.Id);
            if(centerFromDb != null)
            {
                centerFromDb.Name = obj.Name;
                centerFromDb.Description = obj.Name;
                centerFromDb.OpeningTime = obj.OpeningTime;
                centerFromDb.ClosingTime = obj.ClosingTime;
            }
        }
    }
}
