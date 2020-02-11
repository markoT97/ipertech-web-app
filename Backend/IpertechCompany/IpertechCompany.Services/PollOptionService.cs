using IpertechCompany.IRepositories;
using IpertechCompany.IServices;
using IpertechCompany.Models;
using System;
using System.Collections.Generic;

namespace IpertechCompany.Services
{
    public class PollOptionService : IPollOptionService
    {
        private readonly IPollOptionRepository _pollOptionRepository;

        public PollOptionService(IPollOptionRepository pollOptionRepository)
        {
            _pollOptionRepository = pollOptionRepository;
        }

        public PollOption CreatePollOption(PollOption pollOption)
        {
            if (!(pollOption != null))
            {
                throw new ArgumentNullException("pollOption", "Parameter is null.");
            }

            if (!pollOption.IsValid())
            {
                throw new ArgumentException("Missing required properties.");
            }

            return _pollOptionRepository.Insert(pollOption);
        }

        public bool DeletePollOption(Guid pollOptionId)
        {
            return _pollOptionRepository.Delete(pollOptionId);
        }

        public IEnumerable<PollOption> GetByPollId(Guid pollId)
        {
            return _pollOptionRepository.Get(pollId);
        }

        public void UpdatePollOption(PollOption pollOption)
        {
            if (!(pollOption != null))
            {
                throw new ArgumentNullException("pollOption", "Parameter is null.");
            }

            if (!pollOption.IsValid())
            {
                throw new ArgumentException("Missing required properties.");
            }

            _pollOptionRepository.Update(pollOption);
        }
    }
}
