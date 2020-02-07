using System;
using System.Collections.Generic;
using System.Text;
using IpertechCompany.Models;

namespace IpertechCompany.IServices
{
    public interface IOptionVoterService
    {
        int GetNumberOfVotersByPollOptionId(Guid pollOptionId);
        OptionVoter CreateOptionVoter(OptionVoter optionVoter);
    }
}
