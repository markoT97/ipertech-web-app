using System;
using System.Collections.Generic;
using System.Text;
using IpertechCompany.Models;

namespace IpertechCompany.IRepositories
{
    interface ITvChannelRepository
    {
        IEnumerable<TvChannel> GetAll();
        TvChannel Insert(TvChannel tcChannel);
        void Update(TvChannel tvChannel);
        bool Delete(Guid tvChannelId);
    }
}
