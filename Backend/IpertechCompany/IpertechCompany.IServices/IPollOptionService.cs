using System;
using System.Collections.Generic;
using System.Text;
using IpertechCompany.Models;

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
