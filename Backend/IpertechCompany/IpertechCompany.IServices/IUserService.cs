using IpertechCompany.Models;
using System;
using System.Collections.Generic;

namespace IpertechCompany.IServices
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User GetByUserId(Guid userId);
        User GetByUserLogin(UserLogin userLogin);
        User CreateUser(User user);
        string AuthenticateUser(User user);
        void UpdateUser(User user);
        void UpdateUser(UserImage userImage);
        void UpdateUser(UserPassword userPassword);
        bool DeleteUser(Guid userId);
    }
}
