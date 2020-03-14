using IpertechCompany.Models;
using System;

namespace IpertechCompany.IRepositories
{
    public interface IPollRepository
    {
        Poll Get();
        Poll Get(Guid pollId);
        Poll Insert(Poll poll);
        void Update(Poll poll);
        bool Delete(Guid pollId);
    }
}
