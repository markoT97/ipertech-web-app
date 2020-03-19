using IpertechCompany.DbConnection;
using IpertechCompany.DbRepositories;
using IpertechCompany.IRepositories;
using IpertechCompany.Models;
using NUnit.Framework;
using System;
using System.Linq;

namespace IpertechCompany.Tests.Repositories
{
    [TestFixture]
    public class UserMessageRepositoryTests
    {
        private readonly IDbContext _dbContext;
        private readonly IUserMessageRepository _userMessageRepository;

        public UserMessageRepositoryTests()
        {
            _dbContext = new DbContext("Data Source=DESKTOP-883AG4N\\SQLEXPRESS;Initial Catalog=IpertechCompany;Integrated Security=True");
            _userMessageRepository = new UserMessageRepository(_dbContext);
        }

        [Test]
        public void GetAll_WithData_ReturnsPopulatedList()
        {
            Assert.AreEqual(2, _userMessageRepository.GetAll(0, 2).Count());
        }

        [Test]
        public void Insert_WithRequiredFields_ReturnsEighteenUserMessage()
        {
            var userMessage = new UserMessage(new User(Guid.Parse("8D613A6B-AEF0-4B15-95F4-3BB5039F47DE")),
                new Message(Guid.Parse("B18A0A9C-320A-4EC2-BD09-F2E1652E736A")));

            _userMessageRepository.Insert(userMessage);
            Assert.AreEqual(1, _userMessageRepository.GetAll(0, 1).Count());
        }

        [Test]
        public void Insert_NullObject_ExpectsException()
        {
            Assert.Throws<NullReferenceException>(() => _userMessageRepository.Insert(null));
        }

        [Test]
        public void Delete_WhichExists_ReturnsTrue()
        {
            var userMessage = new UserMessage(new User(Guid.Parse("8D613A6B-AEF0-4B15-95F4-3BB5039F47DE")),
                new Message(Guid.Parse("C8D6F372-06F8-40EE-8BC8-A1DFBFDC56FC")));
            Assert.True(_userMessageRepository.Delete(userMessage));
        }

        [Test]
        public void Delete_WhichNotExists_ReturnsFalse()
        {
            var userMessage = new UserMessage(new User(Guid.NewGuid()), new Message(Guid.NewGuid()));
            Assert.False(_userMessageRepository.Delete(userMessage));
        }
    }
}
