using System;
using System.Collections.Generic;
using System.Text;
using IpertechCompany.Models;

namespace IpertechCompany.IRepositories
{
    public interface IMessageRepository
    {
        Message Get(Guid messageId);
        Message Insert(Message message);
        void Update(Message message);
        bool Delete(Guid messageId);
    }
}
