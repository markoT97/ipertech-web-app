using IpertechCompany.IRepositories;
using IpertechCompany.IServices;
using IpertechCompany.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IpertechCompany.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;


        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
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

        public string LoginUser(UserLogin userLogin)
        {
            if (!(userLogin != null))
            {
                throw new ArgumentNullException("user", "Parameter is null.");
            }

            var user = _userRepository.Get(userLogin);

            if (!(user != null))
            {
                throw new InvalidOperationException("Specified user does not exists.");
            }

            var secretCode = "fds6743129hvf89ry42bvfe29ggb59y4hbg948943bufr89b48ibg94";
            var tokenHandler = new JwtSecurityTokenHandler();

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretCode)), "HS256");

            var header = new JwtHeader(signingCredentials);

            var claims = new List<Claim>()
            {
                new Claim("userId", user.UserId.ToString()),
                new Claim("email", user.Email),
                new Claim("role", user.Role),
                new Claim("firstName", user.FirstName),
                new Claim("lastName", user.LastName)
            };
            var dateTimeNow = DateTime.Now;
            var tokenExpirationMinutes = 30;
            var payload = new JwtPayload(null, null, claims, dateTimeNow, dateTimeNow.AddMinutes(tokenExpirationMinutes));

            var token = new JwtSecurityToken(header, payload);
            return tokenHandler.WriteToken(token);
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
        public void UpdateUser(UserImage userImage)
        {
            if (!(userImage != null))
            {
                throw new ArgumentNullException("userImage", "Parameter is null.");
            }

            if (!userImage.IsValid())
            {
                throw new ArgumentException("Missing required properties.");
            }
            _userRepository.Update(userImage);
        }
    }
}
