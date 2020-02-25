using IpertechCompany.IRepositories;
using IpertechCompany.IServices;
using IpertechCompany.Models;
using IpertechCompany.Services;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace IpertechCompany.Tests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;

        public UserServiceTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _userService = new UserService(_userRepository);
        }

        [Test]
        public void CreateUser_NullObject_ExpectsException()
        {
            Assert.Throws<ArgumentNullException>(() => _userService.CreateUser(null));
            _userRepository.DidNotReceive().Insert(null);
        }

        [Test]
        public void CreateUser_WithoutRequitedFields_ExpectsException()
        {
            var user = new User();

            Assert.Throws<ArgumentException>(() => _userService.CreateUser(user));
            _userRepository.DidNotReceive().Insert(user);
        }

        [Test]
        public void CreateUser_WithRequiredFields_ReturnsUser()
        {
            var user = new User(Guid.Parse("8F10E3A8-5B61-42F0-B00A-243F0FA2D228"), new UserContract(Guid.Parse("E352D17F-E719-40F1-B1AE-660510F3DBC4")), "User", "Steva", "Ðubre", "Muški", "steva@gmail.com", "3455666", "123@@", "www/users/steva.png");
            _userRepository.Insert(user).Returns(user);

            var returnedUser = _userService.CreateUser(user);
            _userRepository.Received(1).Insert(user);
            Assert.AreEqual(user, returnedUser);
        }

        [Test]
        public void LoginUser_NullObject_ExpectsException()
        {
            Assert.Throws<ArgumentNullException>(() => _userService.LoginUser(null));
        }

        [Test]
        public void LoginUser_WithRequiredFields_ReturnsToken()
        {
            var user = new User(Guid.Parse("8F10E3A8-5B61-42F0-B00A-243F0FA2D228"), new UserContract(Guid.Parse("E352D17F-E719-40F1-B1AE-660510F3DBC4")), "User", "Steva", "Ðubre", "Muški", "steva@gmail.com", "3455666", "123@@", "www/users/steva.png");

            var userLogin = new UserLogin(user.Email, user.Password);
            _userRepository.Get(userLogin).Returns(user);

            var token = _userService.LoginUser(userLogin);
            var deserializedToken = new JwtSecurityToken(token);

            Assert.AreEqual(user.Role, deserializedToken.Claims.Single(c => c.Type == "role").Value);
        }

        [Test]
        public void UpdateUser_NullObject_ExpectsException()
        {
            Assert.Throws<ArgumentNullException>(() => _userService.UpdateUser(null));
            _userRepository.DidNotReceive().Update(null);
        }

        [Test]
        public void UpdateUser_WithoutRequiredFields_ExpectsException()
        {
            var user = new User();

            Assert.Throws<ArgumentException>(() => _userService.UpdateUser(user));
            _userRepository.DidNotReceive().Update(user);
        }

        [Test]
        public void UpdateUser_WithRequiredFields_ReturnsNothing()
        {
            var user = new User(Guid.Parse("8F10E3A8-5B61-42F0-B00A-243F0FA2D228"), new UserContract(Guid.Parse("E352D17F-E719-40F1-B1AE-660510F3DBC4")), "User", "Steva", "Ðubre", "Muški", "steva@gmail.com", "3455666", "123@@", "www/users/steva.png");
            _userService.UpdateUser(user);
            _userRepository.Received(1).Update(user);
        }


        [Test]
        public void GetUserByUserId_WithoutData_ReturnsEmptyObject()
        {
            var userId = Guid.Parse("C8D6F372-06F8-40EE-8BC8-A1DFBFDC56FC");
            _userRepository.Get(userId)
                .Returns(new User());

            var returnedUser = _userService.GetByUserId(userId);
            Assert.AreEqual(Guid.Empty, returnedUser.UserId);
        }

        [Test]
        public void GetUserByUserId_WithData_ReturnsUser()
        {
            var users = new List<User>()
            {
                new User(Guid.Parse("8F10E3A8-5B61-42F0-B00A-243F0FA2D228"), new UserContract(Guid.Parse("E352D17F-E719-40F1-B1AE-660510F3DBC4")), "User", "Steva", "Ðubre", "Muški", "steva@gmail.com", "3455666", "123@@", "www/users/steva.png"),
                new User(Guid.Parse("8D613A6B-AEF0-4B15-95F4-3BB5039F47DE"), new UserContract(Guid.Parse("2E97AAA3-E364-4C32-BC4C-1895FF066492")), "User", "Marko", "Trickovic", "Muški", "marko@gmail.com", "094857575", "123@@", "www/users/marko.png"),
                new User(Guid.Parse("BAA27786-50D0-4568-95FA-67703D38BEAB"), new UserContract(Guid.Parse("E352D17F-E719-40F1-B1AE-660510F3DBC4")), "User", "Petar", "Petrovic", "Muški", "pera@gmail.com", "04453566", "123@@", "www/users/pera.png"),
                new User(Guid.Parse("38214F65-4001-4200-8621-FF240A2FF4C7"), null, "Admin", "Zdravko", "Herbiko", "Muški", "zdravko@gmail.com", "05433545", "123@@", "www/users/cole.png")
            };
            var userId = Guid.Parse("8F10E3A8-5B61-42F0-B00A-243F0FA2D228");
            _userRepository.Get(userId)
                    .Returns(users.Single(u => u.UserId == userId));

            var returnedUser = _userService.GetByUserId(userId);
            Assert.AreEqual(users.Single(u => u.UserId == userId).Email, returnedUser.Email);
        }

        [Test]
        public void GetAllUsers_WithoutData_ReturnsEmptyList()
        {
            _userRepository.GetAll().Returns(new List<User>());
            Assert.AreEqual(0, _userService.GetAllUsers().Count());
        }

        [Test]
        public void GetAllUsers_WithData_ReturnsPopulatedList()
        {
            var users = new List<User>()
            {
                new User(Guid.Parse("8F10E3A8-5B61-42F0-B00A-243F0FA2D228"), new UserContract(Guid.Parse("E352D17F-E719-40F1-B1AE-660510F3DBC4")), "User", "Steva", "Ðubre", "Muški", "steva@gmail.com", "3455666", "123@@", "www/users/steva.png"),
                new User(Guid.Parse("8D613A6B-AEF0-4B15-95F4-3BB5039F47DE"), new UserContract(Guid.Parse("2E97AAA3-E364-4C32-BC4C-1895FF066492")), "User", "Marko", "Trickovic", "Muški", "steva@gmail.com", "094857575", "123@@", "www/users/marko.png"),
                new User(Guid.Parse("BAA27786-50D0-4568-95FA-67703D38BEAB"), new UserContract(Guid.Parse("E352D17F-E719-40F1-B1AE-660510F3DBC4")), "User", "Petar", "Petrovic", "Muški", "steva@gmail.com", "04453566", "123@@", "www/users/pera.png"),
                new User(Guid.Parse("38214F65-4001-4200-8621-FF240A2FF4C7"), null, "Admin", "Zdravko", "Herbiko", "Muški", "steva@gmail.com", "05433545", "123@@", "www/users/cole.png")
            };
            _userRepository.GetAll().Returns(users);

            Assert.AreEqual(users.Count, _userService.GetAllUsers().Count());
        }
    }
}
