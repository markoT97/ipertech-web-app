using IpertechCompany.IRepositories;
using IpertechCompany.IServices;
using IpertechCompany.Models;
using System;

namespace IpertechCompany.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public Message CreateMessage(Message message)
        {
            if (!(message != null))
            {
                throw new ArgumentNullException("message", "Parameter is null.");
            }

            if (!message.IsValid())
            {
                throw new ArgumentException("Missing required properties.");
            }

            return _messageRepository.Insert(message);
        }

        public bool DeleteMessage(Guid messageId)
        {
            return _messageRepository.Delete(messageId);
        }

        public Message GetByMessageId(Guid messageId)
        {
            return _messageRepository.Get(messageId);
        }

        public void UpdateMessage(Message message)
        {
            if (!(message != null))
            {
                throw new ArgumentNullException("message", "Parameter is null.");
            }

            if (!message.IsValid())
            {
                throw new ArgumentException("Missing required properties.");
            }

            _messageRepository.Update(message);
        }
    }
}
