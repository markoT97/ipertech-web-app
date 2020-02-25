using IpertechCompany.IRepositories;
using IpertechCompany.IServices;
using IpertechCompany.Models;
using System;
using System.Collections.Generic;

namespace IpertechCompany.Services
{
    public class TvPacketTvChannelService : ITvPacketTvChannelService
    {
        private readonly ITvPacketTvChannelRepository _tvPacketTvChannelRepository;

        public TvPacketTvChannelService(ITvPacketTvChannelRepository tvPacketTvChannel)
        {
            _tvPacketTvChannelRepository = tvPacketTvChannel;
        }

        public TvPacketTvChannel CreateTvPacketTvChannel(TvPacketTvChannel tvPacketTvChannel)
        {
            if (!(tvPacketTvChannel != null))
            {
                throw new ArgumentNullException("tvPacketTvChannel", "Parameter is null.");
            }

            if (!tvPacketTvChannel.IsValid())
            {
                throw new ArgumentException("Missing required properties.");
            }

            return _tvPacketTvChannelRepository.Insert(tvPacketTvChannel);
        }

        public bool DeleteTvPacketTvChannel(TvPacketTvChannel tvPacketTvChannel)
        {
            return _tvPacketTvChannelRepository.Delete(tvPacketTvChannel);
        }

        public IEnumerable<TvChannel> GetTvChannelsByTvPacketId(Guid tvPacketId)
        {
            return _tvPacketTvChannelRepository.Get(tvPacketId);
        }
    }
}
