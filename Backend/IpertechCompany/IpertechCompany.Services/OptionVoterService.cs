using IpertechCompany.IRepositories;
using IpertechCompany.IServices;
using IpertechCompany.Models;
using System;

namespace IpertechCompany.Services
{
    public class OptionVoterService : IOptionVoterService
    {
        private readonly IOptionVoterRepository _optionVoterRepository;

        public OptionVoterService(IOptionVoterRepository optionVoterRepository)
        {
            _optionVoterRepository = optionVoterRepository;
        }

        public OptionVoter CreateOptionVoter(OptionVoter optionVoter)
        {
            if (!(optionVoter != null))
            {
                throw new ArgumentNullException("optionVoter", "Parameter is null.");
            }

            if (!optionVoter.IsValid())
            {
                throw new ArgumentException("Missing required properties.");
            }

            return _optionVoterRepository.Insert(optionVoter);
        }

        public int GetNumberOfVotersByPollOptionId(Guid pollOptionId)
        {
            return _optionVoterRepository.Get(pollOptionId);
        }
    }
}
