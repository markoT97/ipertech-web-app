﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IpertechCompany.DbConnection;
using IpertechCompany.DbRepositories;
using IpertechCompany.IRepositories;
using IpertechCompany.Models;
using NUnit.Framework;

namespace IpertechCompany.Tests.Repositories
{
    [TestFixture]
    public class UserRepositoryTests
    {
        private readonly IDbContext _dbContext;
        private readonly IUserRepository _userRepository;

        public UserRepositoryTests()
        {
            _dbContext = new DbContext("Data Source=DESKTOP-883AG4N\\SQLEXPRESS;Initial Catalog=IpertechCompany;Integrated Security=True");
            _userRepository = new UserRepository(_dbContext);
        }
        

        [Test]
        public void GetAll_WithData_ReturnsListOfTwoUsers()
        {
            Assert.AreEqual(2, _userRepository.GetAll().Count());
        }

        [Test]
        public void GetById_WithExistingUser_ReturnsPopulatedList()
        {
            Assert.AreEqual("User", _userRepository.Get(Guid.Parse("8F10E3A8-5B61-42F0-B00A-243F0FA2D228")).Role);
        }
        
        [Test]
        public void GetById_WithoutExistingUser_ReturnsNull()
        {
            Assert.AreEqual(null, _userRepository.Get(Guid.NewGuid()));
        }

        [Test]
        public void Insert_WithRequiredFields_ReturnsListWithThreeUsers()
        {
            var user = new User(Guid.Parse("04822534-5A84-47EA-AF87-81060832CD0A"),
                new UserContract(Guid.Parse("082C90F1-F513-4B2D-ACF1-14B277D6D6C8")),
                "User",
                "Insert F.Name",
                "Insert L.Name",
                "Muški",
                "insert@gmail.com",
                "8467849398",
                "123@@",
                "Insert Location");

            _userRepository.Insert(user);
            Assert.AreEqual(3, _userRepository.GetAll().Count());
        }

        [Test]
        public void Insert_NullObject_ThrowsException()
        {
            Assert.Throws<NullReferenceException>(() => _userRepository.Insert(null));
        }

        [Test]
        public void Update_WithRequiredFields_ReturnsUser()
        {
            var user = new User(Guid.Parse("04822534-5A84-47EA-AF87-81060832CD0A"),
                new UserContract(Guid.Parse("082C90F1-F513-4B2D-ACF1-14B277D6D6C8")),
                "User",
                "Update F.Name",
                "Update L.Name",
                "Muški",
                "update@gmail.com",
                "8444849398",
                "123@@",
                "Update Location");

            _userRepository.Update(user);
            var updatedUser = _userRepository.Get(user.UserId);

            Assert.AreEqual(user.PhoneNumber, updatedUser.PhoneNumber);
        }

        [Test]
        public void Update_NullObject_ExpectsException()
        {
            Assert.Throws<NullReferenceException>(() => _userRepository.Update(null));
        }

        [Test]
        public void Delete_WhichExists_ReturnsTrue()
        {
            Assert.True(_userRepository.Delete(Guid.Parse("8D613A6B-AEF0-4B15-95F4-3BB5039F47DE")));
        }

        [Test]
        public void Delete_WhichNotExists_ReturnsFalse()
        {
            Assert.False(_userRepository.Delete(Guid.NewGuid()));
        }
    }
}
