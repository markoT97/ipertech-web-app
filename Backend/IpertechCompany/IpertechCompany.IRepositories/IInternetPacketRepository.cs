using IpertechCompany.Models;
using System;
using System.Collections.Generic;

namespace IpertechCompany.IRepositories
{
    public interface IInternetPacketRepository
    {
        IEnumerable<InternetPacket> GetAll();
        InternetPacket Insert(InternetPacket internetPacket);
        void Update(InternetPacket internetPacket);
        bool Delete(Guid internetPacketId, Guid internetRouterId);
    }
}
