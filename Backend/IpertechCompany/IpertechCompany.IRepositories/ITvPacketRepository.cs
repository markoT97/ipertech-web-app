﻿using IpertechCompany.Models;
using System;
using System.Collections.Generic;

namespace IpertechCompany.IRepositories
{
    public interface ITvPacketRepository
    {
        IEnumerable<TvPacket> GetAll();
        TvPacket Insert(TvPacket tvPacket);
        void Update(TvPacket tvPacket);
        bool Delete(Guid tvPacketId);
    }
}
