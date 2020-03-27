using IpertechCompany.IRepositories;
using IpertechCompany.IServices;
using IpertechCompany.Models;
using System;
using System.Collections.Generic;

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

        public IEnumerable<OptionVoter> GetNumberOfVotersByPollId(Guid pollId)
        {
            return _optionVoterRepository.Get(pollId);
        }

        public bool CheckIsUserVotedOnPoll(Guid pollId, Guid userId)
        {
            return _optionVoterRepository.Get(pollId, userId);
        }
    }
}
