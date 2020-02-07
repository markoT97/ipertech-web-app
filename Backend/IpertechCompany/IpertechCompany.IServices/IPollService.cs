using System;
using System.Collections.Generic;
using System.Text;
using IpertechCompany.Models;

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
