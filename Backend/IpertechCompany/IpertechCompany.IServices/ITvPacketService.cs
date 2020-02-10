using IpertechCompany.Models;
using System;
using System.Collections.Generic;

namespace IpertechCompany.IServices
{
    public interface ITvPacketService
    {
        IEnumerable<TvPacket> GetAllTvPackets();
        TvPacket CreateTvPacket(TvPacket tvPacket);
        void UpdateTvPacket(TvPacket tvPacket);
        bool DeleteTvPacket(Guid tvPacketId);
    }
}
