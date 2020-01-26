using System;
using System.Collections.Generic;
using System.Text;
using IpertechCompany.Models;

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
