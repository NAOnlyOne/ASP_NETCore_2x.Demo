using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace DAL.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly CustomDbContext _dbContext;

        public UserRepository(CustomDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Add(User user)
        {
            if (user == null)
                return 0;

            if (_dbContext.Users.Any(e => e.Name == user.Name))
                return 0;

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return user.Id;
        }

        public User GetById(int id)
        {
            if (id <= 0)
                return null;

            var user = _dbContext.Users.Find(id);
            return user;
        }

        public User GetByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            var user = _dbContext.Users.SingleOrDefault(e => e.Name == name);
            return user;
        }
    }
}
