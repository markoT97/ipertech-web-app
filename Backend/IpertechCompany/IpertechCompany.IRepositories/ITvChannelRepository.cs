using IpertechCompany.Models;
using System;
using System.Collections.Generic;

namespace IpertechCompany.IRepositories
{
    public interface ITvChannelRepository
    {
        IEnumerable<TvChannel> GetAll();
        TvChannel Insert(TvChannel tvChannel);
        void Update(TvChannel tvChannel);
        bool Delete(Guid tvChannelId);
    }
}
