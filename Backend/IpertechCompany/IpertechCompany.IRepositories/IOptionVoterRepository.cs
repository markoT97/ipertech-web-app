using System;
using System.Collections.Generic;
using System.Text;
using IpertechCompany.Models;

namespace IpertechCompany.IRepositories
{
    interface IOptionVoterRepository
    {
        int Get(Guid pollOptionId);
        OptionVoter Insert(OptionVoter optionVoter);
    }
}
