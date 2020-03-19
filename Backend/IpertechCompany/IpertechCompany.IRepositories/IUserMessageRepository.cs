using IpertechCompany.Models;
using System.Collections.Generic;

namespace IpertechCompany.IRepositories
{
    public interface IUserMessageRepository
    {
        IEnumerable<UserMessage> GetAll(int offset, int numberOfRows);
        int GetAll();
        UserMessage Insert(UserMessage userMessage);
        bool Delete(UserMessage userMessage);
    }
}
