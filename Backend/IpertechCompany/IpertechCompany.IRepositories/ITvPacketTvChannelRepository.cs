using System;
using System.Collections.Generic;
using System.Text;
using IpertechCompany.Models;

namespace IpertechCompany.IRepositories
{
    interface ITvPacketTvChannelRepository
    {
        IEnumerable<TvChannel> Get(Guid tvPacketId);
        TvPacketTvChannel Insert(TvPacketTvChannel tvPacketTvChannel);
        void Update(TvPacketTvChannel tvPacketTvChannel);
        bool Delete(TvPacketTvChannel tvPacketTvChannel);
    }
}
