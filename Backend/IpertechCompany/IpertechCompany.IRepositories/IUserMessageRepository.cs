using System;
using System.Collections.Generic;
using System.Text;
using IpertechCompany.Models;

namespace IpertechCompany.IRepositories
{
    interface IUserMessageRepository
    {
        Message Get(Guid userId);
        Message Insert(UserMessage userMessage);
        void Update(UserMessage userMessage);
        bool Delete(UserMessage userMessage);
    }
}
