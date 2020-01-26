using System;
using System.Collections.Generic;
using System.Text;
using IpertechCompany.Models;

namespace IpertechCompany.IRepositories
{
    public interface IPacketCombinationRepository
    {
        IEnumerable<PacketCombination> GetAll();
        PacketCombination Insert(PacketCombination packetCombination);
        void Update(PacketCombination packetCombination);
        bool Delete(Guid packetCombinationId);
    }
}
