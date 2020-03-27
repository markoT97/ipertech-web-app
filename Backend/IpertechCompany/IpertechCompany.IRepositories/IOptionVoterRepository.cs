using IpertechCompany.Models;
using System;
using System.Collections.Generic;

namespace IpertechCompany.IRepositories
{
    public interface IOptionVoterRepository
    {
        IEnumerable<OptionVoter> Get(Guid pollId);
        bool Get(Guid pollId, Guid userId);
        OptionVoter Insert(OptionVoter optionVoter);
    }
}
