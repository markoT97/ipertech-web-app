using System;
using System.Collections.Generic;
using System.Text;
using IpertechCompany.Models;

namespace IpertechCompany.IServices
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User GetById(Guid userId);
        User CreateUser(User user);
        void UpdateUser(User user);
        bool DeleteUser(Guid userId);
    }
}
