using System;
using System.Collections.Generic;
using System.Text;
using IpertechCompany.Models;

namespace IpertechCompany.IServices
{
    public interface ITvPacketTvChannelService
    {
        IEnumerable<TvChannel> GetTvChannelsByTvPacketId(Guid tvPacketId);
        TvPacketTvChannel CreateTvPacketTvChannel(TvPacketTvChannel tvPacketTvChannel);
        bool DeleteTvPacketTvChannel(TvPacketTvChannel tvPacketTvChannel);
    }
}
