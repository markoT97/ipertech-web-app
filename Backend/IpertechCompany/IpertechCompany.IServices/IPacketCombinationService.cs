using System;
using System.Collections.Generic;
using System.Text;
using IpertechCompany.Models;

namespace IpertechCompany.IServices
{
    public interface IPacketCombinationService
    {
        IEnumerable<PacketCombination> GetAllPacketCombinations();
        PacketCombination CreatePacketCombination(PacketCombination packetCombination);
        void UpdatePacketCombination(PacketCombination packetCombination);
        bool DeletePacketCombination(Guid packetCombinationId);
    }
}
