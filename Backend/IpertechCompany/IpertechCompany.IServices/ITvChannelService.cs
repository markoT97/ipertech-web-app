using IpertechCompany.Models;
using System;
using System.Collections.Generic;

namespace IpertechCompany.IServices
{
    public interface ITvChannelService
    {
        IEnumerable<TvChannel> GetAllTvChannels();
        TvChannel CreateTvChannel(TvChannel tvChannel);
        void UpdateTvChannel(TvChannel tvChannel);
        bool DeleteTvChannel(Guid tvChannelId);
    }
}
