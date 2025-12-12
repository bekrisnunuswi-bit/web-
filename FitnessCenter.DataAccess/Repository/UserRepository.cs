using FitnessCenter.DataAccess.Data;
using FitnessCenter.DataAccess.Repository.IRepository;
using FitnessCenter.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessCenter.DataAccess.Repository
{
    public class UserRepository :Repository<User> , IUserRepository
    {
        private readonly ApplicationDbContext _dbcontext;
        public UserRepository(ApplicationDbContext dbcontext) : base(dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public void Update(User obj)
        {
            _dbcontext.Users.Update(obj);
        }
    }
}
