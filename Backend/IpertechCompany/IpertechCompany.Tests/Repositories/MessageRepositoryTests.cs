using System;
using System.Collections.Generic;
using System.Text;
using IpertechCompany.DbConnection;
using IpertechCompany.DbRepositories;
using IpertechCompany.IRepositories;
using IpertechCompany.Models;
using NUnit.Framework;

namespace IpertechCompany.Tests.Repositories
{
    [TestFixture]
    public class MessageRepositoryTests
    {
        private readonly IDbContext _dbContext;
        private readonly IMessageRepository _messageRepository;

        public MessageRepositoryTests()
        {
            _dbContext = new DbContext("Data Source=DESKTOP-883AG4N\\SQLEXPRESS;Initial Catalog=IpertechCompany;Integrated Security=True");
            _messageRepository = new MessageRepository(_dbContext);
        }

        [Test]
        public void GetById_WithExistingId_ReturnsMessage()
        {
            Assert.AreEqual("Ruter je bez signala", _messageRepository.Get(Guid.Parse("C8D6F372-06F8-40EE-8BC8-A1DFBFDC56FC")).Title);
        }
        
        [Test]
        public void GetById_WithoutExistingId_ReturnsNull()
        {
            Assert.AreEqual(null, _messageRepository.Get(Guid.NewGuid()));
        }

        [Test]
        public void Insert_WithRequiredFields_ReturnsMessage()
        {
            var message = new Message(Guid.NewGuid(), "Insert Title", "Insert Content", "Insert");

            _messageRepository.Insert(message);
            Assert.AreEqual(message.Title, _messageRepository.Get(message.MessageId).Title);
        }

        [Test]
        public void Insert_NullObject_ExpectsException()
        {
            Assert.Throws<NullReferenceException>(() => _messageRepository.Insert(null));
        }

        [Test]
        public void Update_WithRequiredFields_ReturnsMessage()
        {
            var message = new Message(Guid.Parse("C8D6F372-06F8-40EE-8BC8-A1DFBFDC56FC"), "Update Title", "Update Content", "Update");

            _messageRepository.Update(message);
            var updatedMessage = _messageRepository
                .Get(message.MessageId);

            Assert.AreEqual(message.Title, updatedMessage.Title);
        }

        [Test]
        public void Update_NullObject_ExpectsException()
        {
            Assert.Throws<NullReferenceException>(() => _messageRepository.Update(null));
        }

        [Test]
        public void Delete_WhichExists_ReturnsTrue()
        {
            Assert.True(_messageRepository.Delete(Guid.Parse("2B78F0D1-FFFA-4723-AD87-C9E2532ACD70")));
        }

        [Test]
        public void Delete_WhichNotExists_ReturnsFalse()
        {
            Assert.False(_messageRepository.Delete(Guid.NewGuid()));
        }
    }
}
