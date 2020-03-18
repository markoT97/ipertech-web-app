using IpertechCompany.Models;
using System;
using System.Collections.Generic;

namespace IpertechCompany.IServices
{
    public interface IUserMessageService
    {
        IEnumerable<Message> GetMessagesByUserId(Guid userId, int offset, int numberOfRows);
        int GetTotalNumberOfMessagesByUserId(Guid userId);
        UserMessage CreateUserMessage(UserMessage userMessage);
        bool DeleteUserMessage(UserMessage userMessage);
    }
}
