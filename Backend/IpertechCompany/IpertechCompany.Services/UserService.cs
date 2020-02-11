using IpertechCompany.IRepositories;
using IpertechCompany.IServices;
using IpertechCompany.Models;
using System;
using System.Collections.Generic;

namespace IpertechCompany.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User CreateUser(User user)
        {
            if (!(user != null))
            {
                throw new ArgumentNullException("user", "Parameter is null.");
            }

            if (!user.IsValid())
            {
                throw new ArgumentException("Missing required properties.");
            }

            return _userRepository.Insert(user);
        }

        public bool DeleteUser(Guid userId)
        {
            return _userRepository.Delete(userId);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }

        public User GetByUserId(Guid userId)
        {
            return _userRepository.Get(userId);
        }

        public void UpdateUser(User user)
        {
            if (!(user != null))
            {
                throw new ArgumentNullException("user", "Parameter is null.");
            }

            if (!user.IsValid())
            {
                throw new ArgumentException("Missing required properties.");
            }
            _userRepository.Update(user);
        }
    }
}
