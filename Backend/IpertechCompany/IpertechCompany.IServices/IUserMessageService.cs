using IpertechCompany.Models;
using System.Collections.Generic;

namespace IpertechCompany.IServices
{
    public interface IUserMessageService
    {
        IEnumerable<UserMessage> GetAllUserMessages(int offset, int numberOfRows);
        int GetTotalNumberOfMessages();
        UserMessage CreateUserMessage(UserMessage userMessage);
        bool DeleteUserMessage(UserMessage userMessage);
    }
}
