using System;
using System.Collections.Generic;
using System.Text;
using IpertechCompany.Models;

namespace IpertechCompany.IRepositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User Get(Guid userId);
        User Insert(User user);
        void Update(User user);
        bool Delete(Guid userId);
    }
}
