using System;
using System.Collections.Generic;
using System.Text;
using IpertechCompany.Models;

namespace IpertechCompany.IServices
{
    public interface IInternetPacketService
    {
        IEnumerable<InternetPacket> GetAllInternetPackets();
        InternetPacket CreateInternetPacket(InternetPacket internetPacket);
        void UpdateInternetPacket(InternetPacket internetPacket);
        bool DeleteInternetPacket(Guid internetPacketId, Guid internetRouterId);
    }
}
