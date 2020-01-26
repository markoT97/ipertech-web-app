using System;
using System.Collections.Generic;
using System.Text;
using IpertechCompany.Models;

namespace IpertechCompany.IRepositories
{
    interface IInternetPacketRepository
    {
        IEnumerable<InternetPacket> GetAll();
        InternetPacket Insert(InternetPacket internetPacket);
        void Update(InternetPacket internetPacket);
        bool Delete(Guid internetPacketId);
    }
}
