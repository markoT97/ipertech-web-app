using IpertechCompany.IRepositories;
using IpertechCompany.IServices;
using IpertechCompany.Models;
using System;
using System.Collections.Generic;

namespace IpertechCompany.Services
{
    public class UserMessageService : IUserMessageService
    {
        private readonly IUserMessageRepository _userMessageRepository;

        public UserMessageService(IUserMessageRepository userMessageRepository)
        {
            _userMessageRepository = userMessageRepository;
        }

        public UserMessage CreateUserMessage(UserMessage userMessage)
        {
            if (!(userMessage != null))
            {
                throw new ArgumentNullException("userMessage", "Parameter is null.");
            }

            if (!userMessage.IsValid())
            {
                throw new ArgumentException("Missing required properties.");
            }

            return _userMessageRepository.Insert(userMessage);
        }

        public bool DeleteUserMessage(UserMessage userMessage)
        {
            return _userMessageRepository.Delete(userMessage);
        }

        public IEnumerable<Message> GetMessagesByUserId(Guid userId)
        {
            return _userMessageRepository.Get(userId);
        }
    }
}
