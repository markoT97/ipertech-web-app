using IpertechCompany.Models;
using System;

namespace IpertechCompany.IServices
{
    public interface IOptionVoterService
    {
        int GetNumberOfVotersByPollOptionId(Guid pollOptionId);
        OptionVoter CreateOptionVoter(OptionVoter optionVoter);
    }
}
