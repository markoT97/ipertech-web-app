using IpertechCompany.Models;
using System;

namespace IpertechCompany.IServices
{
    public interface IMessageService
    {
        Message GetByMessageId(Guid messageId);
        Message CreateMessage(Message message);
        void UpdateMessage(Message message);
        bool DeleteMessage(Guid messageId);
    }
}
