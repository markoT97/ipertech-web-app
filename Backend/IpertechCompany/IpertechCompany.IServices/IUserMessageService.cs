using System;
using System.Collections.Generic;
using System.Text;
using IpertechCompany.Models;

namespace IpertechCompany.IServices
{
    public interface IUserMessageService
    {
        IEnumerable<Message> GetMessagesByUserId(Guid userId);
        UserMessage CreateUserMessage(UserMessage userMessage);
        bool DeleteUserMessage(UserMessage userMessage);
    }
}
