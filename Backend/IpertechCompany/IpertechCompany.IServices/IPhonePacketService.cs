using System;
using System.Collections.Generic;
using System.Text;
using IpertechCompany.Models;

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
