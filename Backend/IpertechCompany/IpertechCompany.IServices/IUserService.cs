using IpertechCompany.Models;
using System;
using System.Collections.Generic;

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
