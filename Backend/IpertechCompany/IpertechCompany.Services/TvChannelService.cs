using IpertechCompany.IRepositories;
using IpertechCompany.IServices;
using IpertechCompany.Models;
using System;
using System.Collections.Generic;

namespace IpertechCompany.Services
{
    public class TvChannelService : ITvChannelService
    {
        private readonly ITvChannelRepository _tvChannelRepository;

        public TvChannelService(ITvChannelRepository tvChannel)
        {
            _tvChannelRepository = tvChannel;
        }

        public TvChannel CreateTvChannel(TvChannel tvChannel)
        {
            if (!(tvChannel != null))
            {
                throw new ArgumentNullException("tvChannel", "Parameter is null.");
            }

            if (!tvChannel.IsValid())
            {
                throw new ArgumentException("Missing required properties.");
            }

            return _tvChannelRepository.Insert(tvChannel);
        }

        public bool DeleteTvChannel(Guid tvChannelId)
        {
            return _tvChannelRepository.Delete(tvChannelId);
        }

        public IEnumerable<TvChannel> GetAllTvChannels()
        {
            return _tvChannelRepository.GetAll();
        }

        public void UpdateTvChannel(TvChannel tvChannel)
        {
            if (!(tvChannel != null))
            {
                throw new ArgumentNullException("tvChannel", "Parameter is null.");
            }

            if (!tvChannel.IsValid())
            {
                throw new ArgumentException("Missing required properties.");
            }
            _tvChannelRepository.Update(tvChannel);
        }
    }
}
