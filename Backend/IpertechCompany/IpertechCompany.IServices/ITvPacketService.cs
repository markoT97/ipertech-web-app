using System;
using System.Collections.Generic;
using System.Text;
using IpertechCompany.Models;

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
