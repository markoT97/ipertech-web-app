using IpertechCompany.Models;
using System;
using System.Collections.Generic;

namespace IpertechCompany.IRepositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User Get(Guid userId);
        User Get(string email, string password);
        User Insert(User user);
        void Update(User user);
        bool Delete(Guid userId);
    }
}
