using IpertechCompany.Models;
using System;
using System.Collections.Generic;

namespace IpertechCompany.IRepositories
{
    public interface ITvPacketTvChannelRepository
    {
        IEnumerable<TvChannel> Get(Guid tvPacketId);
        TvPacketTvChannel Insert(TvPacketTvChannel tvPacketTvChannel);
        bool Delete(TvPacketTvChannel tvPacketTvChannel);
    }
}
