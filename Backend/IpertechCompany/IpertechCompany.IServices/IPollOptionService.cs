using IpertechCompany.Models;
using System;
using System.Collections.Generic;

namespace IpertechCompany.IServices
{
    public interface IPollOptionService
    {
        IEnumerable<PollOption> GetByPollId(Guid pollId);
        PollOption CreatePollOption(PollOption pollOption);
        void UpdatePollOption(PollOption pollOption);
        bool DeletePollOption(Guid pollOptionId);
    }
}
