using IpertechCompany.Models;
using System;
using System.Collections.Generic;

namespace IpertechCompany.IServices
{
    public interface ITvPacketTvChannelService
    {
        IEnumerable<TvChannel> GetTvChannelsByTvPacketId(Guid tvPacketId);
        TvPacketTvChannel CreateTvPacketTvChannel(TvPacketTvChannel tvPacketTvChannel);
        bool DeleteTvPacketTvChannel(TvPacketTvChannel tvPacketTvChannel);
    }
}
