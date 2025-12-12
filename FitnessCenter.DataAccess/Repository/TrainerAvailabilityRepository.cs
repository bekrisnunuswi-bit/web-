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
    public class TrainerAvailabilityRepository : ITrainerAvailabilityRepository
    {
        private readonly ApplicationDbContext _dbcontext;
        public TrainerAvailabilityRepository(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public TrainerAvailability Get(Expression<Func<TrainerAvailability, bool>> filter, string? includeProperties = null)
        {
            IQueryable<TrainerAvailability> query = _dbcontext.TrainerAvailabilities;
            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault();
        }

        public IEnumerable<TrainerAvailability> GetAll(Expression<Func<TrainerAvailability, bool>>? filter, string? includeProperties = null)
        {
            IQueryable<TrainerAvailability> query = _dbcontext.TrainerAvailabilities;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.ToList();
        }

        public void Update(TrainerAvailability obj)
        {
            _dbcontext.Update(obj);
        }
    }
}
