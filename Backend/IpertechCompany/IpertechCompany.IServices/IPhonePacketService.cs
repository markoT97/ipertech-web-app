using IpertechCompany.Models;
using System;
using System.Collections.Generic;

namespace IpertechCompany.IServices
{
    public interface IPhonePacketService
    {
        IEnumerable<PhonePacket> GetAllPhonePackets();
        PhonePacket CreatePhonePacket(PhonePacket phonePacket);
        void UpdatePhonePacket(PhonePacket phonePacket);
        bool DeletePhonePacket(Guid phonePacket);
    }
}
