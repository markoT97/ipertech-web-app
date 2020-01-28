using System;
using System.Collections.Generic;
using System.Text;
using IpertechCompany.Models;

namespace IpertechCompany.IRepositories
{
    public interface IUserMessageRepository
    {
        Message Get(Guid userId);
        UserMessage Insert(UserMessage userMessage);
        bool Delete(UserMessage userMessage);
    }
}
