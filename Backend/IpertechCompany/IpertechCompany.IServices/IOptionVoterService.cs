using IpertechCompany.Models;
using System;
using System.Collections.Generic;

namespace IpertechCompany.IServices
{
    public interface IOptionVoterService
    {
        IEnumerable<OptionVoter> GetNumberOfVotersByPollId(Guid pollId);
        bool CheckIsUserVotedOnPoll(Guid pollId, Guid userId);
        OptionVoter CreateOptionVoter(OptionVoter optionVoter);
    }
}
