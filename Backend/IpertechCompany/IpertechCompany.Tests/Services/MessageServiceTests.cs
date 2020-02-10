using IpertechCompany.IRepositories;
using IpertechCompany.IServices;
using IpertechCompany.Models;
using IpertechCompany.Services;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IpertechCompany.Tests.Services
{
    [TestFixture]
    public class MessageServiceTests
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMessageService _messageService;

        public MessageServiceTests()
        {
            _messageRepository = Substitute.For<IMessageRepository>();
            _messageService = new MessageService(_messageRepository);
        }

        [Test]
        public void CreateMessage_NullObject_ExpectsException()
        {
            _messageRepository.Insert(null).Throws<NullReferenceException>();

            Assert.Throws<ArgumentNullException>(() => _messageService.CreateMessage(null));
            _messageRepository.DidNotReceive().Insert(null);
        }

        [Test]
        public void CreateMessage_WithoutRequiredFields_ExpectsException()
        {
            var message = new Message(Guid.NewGuid());
            _messageRepository.Insert(message).Returns(message);

            Assert.Throws<ArgumentException>(() => _messageService.CreateMessage(message));
            _messageRepository.DidNotReceive().Insert(message);
        }

        [Test]
        public void CreateMessage_WithRequiredFields_ReturnsMessage()
        {
            var message = new Message(Guid.Parse("C8D6F372-06F8-40EE-8BC8-A1DFBFDC56FC"),
                "Ruter je bez signala",
                "Svi kablovi su povezani, ali racunar nece da ucita stranicu...", "Problem");
            _messageRepository.Insert(message).Returns(message);

            var returnedMessage = _messageService.CreateMessage(message);
            _messageRepository.Received(1).Insert(message);
            Assert.AreEqual(message, returnedMessage);
        }

        [Test]
        public void UpdateMessage_NullObject_ExpectsException()
        {
            Assert.Throws<ArgumentNullException>(() => _messageService.UpdateMessage(null));
            _messageRepository.DidNotReceive().Update(null);
        }

        [Test]
        public void UpdateMessage_WithoutRequiredFields_ExpectsException()
        {
            var message = new Message();

            Assert.Throws<ArgumentException>(() => _messageService.UpdateMessage(message));
            _messageRepository.DidNotReceive().Update(message);
        }

        [Test]
        public void UpdateMessage_WithRequiredFields_ReturnsNothing()
        {
            var message = new Message(Guid.Parse("C8D6F372-06F8-40EE-8BC8-A1DFBFDC56FC"),
                "Ruter je bez signala",
                "Svi kablovi su povezani, ali racunar nece da ucita stranicu...", "Problem");
            _messageService.UpdateMessage(message);
            _messageRepository.Received(1).Update(message);
        }

        [Test]
        public void DeleteMessage_WhichNotExists_ReturnsTrue()
        {
            var messageId = Guid.NewGuid();

            _messageRepository.Delete(messageId).Returns(false);
            Assert.False(_messageService.DeleteMessage(messageId));
        }

        [Test]
        public void DeleteMessage_WhichExists_ReturnsTrue()
        {
            var messageId = Guid.Parse("C8D6F372-06F8-40EE-8BC8-A1DFBFDC56FC");

            _messageRepository.Delete(messageId).Returns(true);
            Assert.True(_messageService.DeleteMessage(messageId));
        }

        [Test]
        public void GetMessageByMessageId_WithoutData_ReturnsNullObject()
        {
            var messageId = Guid.Parse("C8D6F372-06F8-40EE-8BC8-A1DFBFDC56FC");
            _messageRepository.Get(messageId)
                .Returns(new Message());

            var returnedMessage = _messageService.GetByMessageId(messageId);
            Assert.AreEqual(Guid.Empty, returnedMessage.MessageId);
        }

        [Test]
        public void GetMessageByMessageId_WithData_ReturnsMessage()
        {
            var messages = new List<Message>()
            {
                new Message(Guid.Parse("C8D6F372-06F8-40EE-8BC8-A1DFBFDC56FC"),
                    "Ruter je bez signala",
                    "Svi kablovi su povezani, ali racunar nece da ucita stranicu...", "Problem"),
                new Message(Guid.Parse("2B78F0D1-FFFA-4723-AD87-C9E2532ACD70"),
                    "Slanje utisaka",
                    "Slanje utisaka je pocelo da radi!", "Utisak"),
                new Message(Guid.Parse("C1851B9D-9624-44D0-86D4-CE10EC026A43"),
                    "TV gubi signal",
                    "Sve je povezano, medutim slika se cesto gubi.", "Problem"),
                new Message(Guid.Parse("B18A0A9C-320A-4EC2-BD09-F2E1652E736A"),
                    "Proba",
                    "Proba radi!", "Utisak")
            };
            var messageId = Guid.Parse("C8D6F372-06F8-40EE-8BC8-A1DFBFDC56FC");
            _messageRepository.Get(messageId)
                .Returns(messages.Single(ir => ir.MessageId == messageId));

            var returnedMessage = _messageService.GetByMessageId(messageId);
            Assert.AreEqual(messages.Single(ir => ir.MessageId == messageId).Title, returnedMessage.Title);
        }
    }
}
