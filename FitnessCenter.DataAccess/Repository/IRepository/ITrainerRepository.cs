using FitnessCenter.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessCenter.DataAccess.Repository.IRepository
{
    public interface ITrainerRepository:IRepository<Trainer>
    {
       void Update(Trainer obj);
    }
}
