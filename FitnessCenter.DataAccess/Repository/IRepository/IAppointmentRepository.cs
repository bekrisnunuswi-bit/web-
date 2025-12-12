using FitnessCenter.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace FitnessCenter.DataAccess.Repository.IRepository
{
    public interface IAppointmentRepository
    {
        Appointment Get(Expression<Func<Appointment, bool>>? filter = null,string? includeProperties = null);
        IEnumerable<Appointment> GetAll(Expression<Func<Appointment, bool>>? filter = null, string? includeProperties = null);
        void Add(Appointment entity);
        void Update(Appointment obj);
    }
}
