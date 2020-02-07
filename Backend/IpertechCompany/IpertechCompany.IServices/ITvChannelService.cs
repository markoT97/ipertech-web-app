using System;
using System.Collections.Generic;
using System.Text;
using IpertechCompany.Models;

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
