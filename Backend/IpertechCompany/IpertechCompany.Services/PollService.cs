using IpertechCompany.IRepositories;
using IpertechCompany.IServices;
using IpertechCompany.Models;
using System;

namespace IpertechCompany.Services
{
    public class PollService : IPollService
    {
        private readonly IPollRepository _pollRepository;

        public PollService(IPollRepository pollRepository)
        {
            _pollRepository = pollRepository;
        }

        public Poll CreatePoll(Poll poll)
        {
            if (!(poll != null))
            {
                throw new ArgumentNullException("poll", "Parameter is null.");
            }

            if (!poll.IsValid())
            {
                throw new ArgumentException("Missing required properties.");
            }

            return _pollRepository.Insert(poll);
        }

        public bool DeletePoll(Guid pollId)
        {
            return _pollRepository.Delete(pollId);
        }

        public Poll GetLatestPoll()
        {
            return _pollRepository.Get();
        }

        public Poll GetByPollId(Guid pollId)
        {
            return _pollRepository.Get(pollId);
        }

        public void UpdatePoll(Poll poll)
        {
            if (!(poll != null))
            {
                throw new ArgumentNullException("poll", "Parameter is null.");
            }

            if (!poll.IsValid())
            {
                throw new ArgumentException("Missing required properties.");
            }

            _pollRepository.Update(poll);
        }
    }
}
