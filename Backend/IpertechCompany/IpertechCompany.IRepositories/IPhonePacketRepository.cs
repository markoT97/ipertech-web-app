using IpertechCompany.Models;
using System;
using System.Collections.Generic;

namespace IpertechCompany.IRepositories
{
    public interface IPhonePacketRepository
    {
        IEnumerable<PhonePacket> GetAll();
        PhonePacket Insert(PhonePacket phonePacket);
        void Update(PhonePacket phonePacket);
        bool Delete(Guid phonePacketId);
    }
}
