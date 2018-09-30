using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository
{
    public interface IUserRepository
    {
        User GetById(int id);

        User GetByName(string name);

        int Add(User user);
    }
}
