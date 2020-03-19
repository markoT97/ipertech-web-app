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
    public class UserMessageServiceTests
    {
        private readonly IUserMessageRepository _userMessageRepository;
        private readonly IUserMessageService _userMessageService;

        public UserMessageServiceTests()
        {
            _userMessageRepository = Substitute.For<IUserMessageRepository>();
            _userMessageService = new UserMessageService(_userMessageRepository);
        }

        [Test]
        public void CreateUserMessage_NullObject_ExpectsException()
        {
            _userMessageRepository.Insert(null).Throws<NullReferenceException>();

            Assert.Throws<ArgumentNullException>(() => _userMessageService.CreateUserMessage(null));
            _userMessageRepository.DidNotReceive().Insert(null);
        }

        [Test]
        public void CreateUserMessage_WithoutRequiredFields_ExpectsException()
        {
            var userMessage = new UserMessage();
            userMessage.User = new User();

            _userMessageRepository.Insert(userMessage).Returns(userMessage);

            Assert.Throws<ArgumentException>(() => _userMessageService.CreateUserMessage(userMessage));
            _userMessageRepository.DidNotReceive().Insert(userMessage);
        }

        [Test]
        public void CreateUserMessage_WithRequiredFields_ReturnsUserMessage()
        {
            var userMessage = new UserMessage(new User(Guid.Parse("8F10E3A8-5B61-42F0-B00A-243F0FA2D228")),
                    new Message(Guid.Parse("C1851B9D-9624-44D0-86D4-CE10EC026A43")));
            _userMessageRepository.Insert(userMessage).Returns(userMessage);

            var returnedUserMessage = _userMessageService.CreateUserMessage(userMessage);
            _userMessageRepository.Received(1).Insert(userMessage);
            Assert.AreEqual(userMessage, returnedUserMessage);
        }

        [Test]
        public void DeleteUserMessage_WhichNotExists_ReturnsTrue()
        {
            var userMessage = new UserMessage();

            _userMessageRepository.Delete(userMessage).Returns(false);
            Assert.False(_userMessageService.DeleteUserMessage(userMessage));
        }

        [Test]
        public void DeleteUserMessage_WhichExists_ReturnsTrue()
        {
            var userMessage = new UserMessage(new User(Guid.Parse("8F10E3A8-5B61-42F0-B00A-243F0FA2D228")),
                    new Message(Guid.Parse("C1851B9D-9624-44D0-86D4-CE10EC026A43")));

            _userMessageRepository.Delete(userMessage).Returns(true);
            Assert.True(_userMessageService.DeleteUserMessage(userMessage));
        }

        [Test]
        public void GetAllMessages_WithData_ReturnsPopulatedList()
        {
            var userMessages = new List<UserMessage>()
            {
                new UserMessage(new User(Guid.Parse("8F10E3A8-5B61-42F0-B00A-243F0FA2D228")),
                    new Message(Guid.Parse("C1851B9D-9624-44D0-86D4-CE10EC026A43"))),
                new UserMessage(new User(Guid.Parse("8D613A6B-AEF0-4B15-95F4-3BB5039F47DE")),
                    new Message(Guid.Parse("C8D6F372-06F8-40EE-8BC8-A1DFBFDC56FC"))),
                new UserMessage(new User(Guid.Parse("8D613A6B-AEF0-4B15-95F4-3BB5039F47DE")),
                    new Message(Guid.Parse("2B78F0D1-FFFA-4723-AD87-C9E2532ACD70")))
            };

            var messages = new List<Message>()
            {
                new Message(Guid.Parse("C8D6F372-06F8-40EE-8BC8-A1DFBFDC56FC"), "Ruter je bez signala", "Svi kablovi su povezani, ali racunar nece da ucita stranicu...", DateTime.Parse("2020-02-25 22:53:56.917"), "Problem"),
                new Message(Guid.Parse("2B78F0D1-FFFA-4723-AD87-C9E2532ACD70"), "Slanje utisaka", "Slanje utisaka je pocelo da radi!", DateTime.Parse("2020-02-25 22:53:56.917"), "Utisak"),
                new Message(Guid.Parse("C1851B9D-9624-44D0-86D4-CE10EC026A43"), "TV gubi signal", "Sve je povezano, međutim slika se cesto gubi.", DateTime.Parse("2020-02-25 22:53:56.917"), "Problem")
            };

            var userId = Guid.Parse("8D613A6B-AEF0-4B15-95F4-3BB5039F47DE");

            var foundedUserMessages = new List<UserMessage>();

            foreach (UserMessage userMessage in userMessages)
            {
                if (userMessage.User.UserId.Equals(userId))
                {
                    foundedUserMessages.Add(userMessage);
                }
            }

            var foundedMessages = new List<Message>();

            foreach (Message message in messages)
            {
                foreach (UserMessage userMessage in foundedUserMessages)
                {
                    if (message.MessageId.Equals(userMessage.Message.MessageId))
                    {
                        foundedMessages.Add(message);
                    }
                }
            }

            _userMessageRepository.GetAll(0, 10)
                        .Returns(foundedUserMessages);

            var returnedMessages = _userMessageService.GetAllUserMessages(0, 10);
            Assert.AreEqual(foundedMessages.Count(), returnedMessages.Count());
        }
    }
}
