using FitnessCenter.DataAccess.Data;
using FitnessCenter.DataAccess.Repository.IRepository;
using FitnessCenter.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessCenter.DataAccess.Repository
{
    public class TrainerRepository : Repository<Trainer>, ITrainerRepository
    {
        private readonly ApplicationDbContext _dbcontext;
        public TrainerRepository(ApplicationDbContext dbcontext):base(dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public void Update(Trainer obj)
        {
            _dbcontext.Trainers.Update(obj);
        }
    }
}
