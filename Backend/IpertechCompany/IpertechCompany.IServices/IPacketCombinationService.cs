using IpertechCompany.Models;
using System;
using System.Collections.Generic;

namespace IpertechCompany.IServices
{
    public interface IPacketCombinationService
    {
        IEnumerable<PacketCombination> GetAllPacketCombinations();
        PacketCombination GetPacketCombinationByInternetAndTvAndPhonePacketId(Guid internetPacketId, Guid? tvPacketId, Guid? phonePacketId);
        PacketCombination CreatePacketCombination(PacketCombination packetCombination);
        void UpdatePacketCombination(PacketCombination packetCombination);
        bool DeletePacketCombination(Guid packetCombinationId);
    }
}
