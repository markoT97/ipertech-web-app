using IpertechCompany.Models;
using System;
using System.Collections.Generic;

namespace IpertechCompany.IServices
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User GetByUserId(Guid userId);
        User CreateUser(User user);
        string LoginUser(string email, string password);
        void UpdateUser(User user);
        bool DeleteUser(Guid userId);
    }
}
