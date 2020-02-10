using IpertechCompany.IRepositories;
using IpertechCompany.IServices;
using IpertechCompany.Models;
using System;
using System.Collections.Generic;

namespace IpertechCompany.Services
{
    public class InternetPacketService : IInternetPacketService
    {
        private readonly IInternetPacketRepository _internetPacketRepository;

        public InternetPacketService(IInternetPacketRepository internetPacket)
        {
            _internetPacketRepository = internetPacket;
        }

        public InternetPacket CreateInternetPacket(InternetPacket internetPacket)
        {
            if (!(internetPacket != null))
            {
                throw new ArgumentNullException("internetPacket", "Parameter is null.");
            }

            if (!internetPacket.IsValid())
            {
                throw new ArgumentException("Missing required properties.");
            }

            return _internetPacketRepository.Insert(internetPacket);
        }

        public bool DeleteInternetPacket(Guid internetPacketId, Guid internetRouterId)
        {
            return _internetPacketRepository.Delete(internetPacketId, internetRouterId);
        }

        public IEnumerable<InternetPacket> GetAllInternetPackets()
        {
            return _internetPacketRepository.GetAll();
        }

        public void UpdateInternetPacket(InternetPacket internetPacket)
        {
            if (!(internetPacket != null))
            {
                throw new ArgumentNullException("internetPacket", "Parameter is null.");
            }

            if (!internetPacket.IsValid())
            {
                throw new ArgumentException("Missing required properties.");
            }
            _internetPacketRepository.Update(internetPacket);
        }
    }
}
