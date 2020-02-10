using IpertechCompany.Models;
using System;

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
