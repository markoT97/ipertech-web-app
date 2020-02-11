using IpertechCompany.IRepositories;
using IpertechCompany.IServices;
using IpertechCompany.Models;
using System;
using System.Collections.Generic;

namespace IpertechCompany.Services
{
    public class PacketCombinationService : IPacketCombinationService
    {
        private readonly IPacketCombinationRepository _packetCombinationRepository;

        public PacketCombinationService(IPacketCombinationRepository packetCombination)
        {
            _packetCombinationRepository = packetCombination;
        }

        public PacketCombination CreatePacketCombination(PacketCombination packetCombination)
        {
            if (!(packetCombination != null))
            {
                throw new ArgumentNullException("packetCombination", "Parameter is null.");
            }

            if (!packetCombination.IsValid())
            {
                throw new ArgumentException("Missing required properties.");
            }

            return _packetCombinationRepository.Insert(packetCombination);
        }

        public bool DeletePacketCombination(Guid packetCombinationId)
        {
            return _packetCombinationRepository.Delete(packetCombinationId);
        }

        public IEnumerable<PacketCombination> GetAllPacketCombinations()
        {
            return _packetCombinationRepository.GetAll();
        }

        public void UpdatePacketCombination(PacketCombination packetCombination)
        {
            if (!(packetCombination != null))
            {
                throw new ArgumentNullException("packetCombination", "Parameter is null.");
            }

            if (!packetCombination.IsValid())
            {
                throw new ArgumentException("Missing required properties.");
            }
            _packetCombinationRepository.Update(packetCombination);
        }
    }
}
