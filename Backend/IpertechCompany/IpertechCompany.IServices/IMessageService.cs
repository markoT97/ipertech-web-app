using System;
using System.Collections.Generic;
using System.Text;
using IpertechCompany.Models;

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
