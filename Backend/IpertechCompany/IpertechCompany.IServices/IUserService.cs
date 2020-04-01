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
        string LoginUser(UserLogin userLogin);
        void UpdateUser(User user);
        void UpdateUser(UserImage userImage);
        bool DeleteUser(Guid userId);
    }
}
