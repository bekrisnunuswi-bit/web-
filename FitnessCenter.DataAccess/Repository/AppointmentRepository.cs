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
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _dbcontext;
        public AppointmentRepository(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public Appointment Get(Expression<Func<Appointment, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<Appointment> query = _dbcontext.Appointments;

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
            return query.FirstOrDefault();
        }

        public IEnumerable<Appointment> GetAll(Expression<Func<Appointment, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<Appointment> query = _dbcontext.Appointments;

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
        public void Add(Appointment entity)
        {
            _dbcontext.Appointments.Add(entity);
        }
        public void Update(Appointment obj)
        {
            _dbcontext.Appointments.Update(obj);
        }
    }
}
