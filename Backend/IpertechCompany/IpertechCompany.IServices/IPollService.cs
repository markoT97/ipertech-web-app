using IpertechCompany.Models;
using System;

namespace IpertechCompany.IServices
{
    public interface IPollService
    {
        Poll GetByPollId(Guid pollId);
        Poll CreatePoll(Poll poll);
        void UpdatePoll(Poll poll);
        bool DeletePoll(Guid pollId);
    }
}
