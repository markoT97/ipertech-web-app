using IpertechCompany.Models;
using System;

namespace IpertechCompany.IRepositories
{
    public interface IOptionVoterRepository
    {
        int Get(Guid pollOptionId);
        OptionVoter Insert(OptionVoter optionVoter);
    }
}
