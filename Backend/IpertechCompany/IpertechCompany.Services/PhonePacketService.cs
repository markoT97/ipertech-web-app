using IpertechCompany.IRepositories;
using IpertechCompany.IServices;
using IpertechCompany.Models;
using System;
using System.Collections.Generic;

namespace IpertechCompany.Services
{
    public class PhonePacketService : IPhonePacketService
    {
        private readonly IPhonePacketRepository _phonePacketRepository;

        public PhonePacketService(IPhonePacketRepository phonePacket)
        {
            _phonePacketRepository = phonePacket;
        }

        public PhonePacket CreatePhonePacket(PhonePacket phonePacket)
        {
            if (!(phonePacket != null))
            {
                throw new ArgumentNullException("phonePacket", "Parameter is null.");
            }

            if (!phonePacket.IsValid())
            {
                throw new ArgumentException("Missing required properties.");
            }

            return _phonePacketRepository.Insert(phonePacket);
        }

        public bool DeletePhonePacket(Guid phonePacketId)
        {
            return _phonePacketRepository.Delete(phonePacketId);
        }

        public IEnumerable<PhonePacket> GetAllPhonePackets()
        {
            return _phonePacketRepository.GetAll();
        }

        public void UpdatePhonePacket(PhonePacket phonePacket)
        {
            if (!(phonePacket != null))
            {
                throw new ArgumentNullException("phonePacket", "Parameter is null.");
            }

            if (!phonePacket.IsValid())
            {
                throw new ArgumentException("Missing required properties.");
            }
            _phonePacketRepository.Update(phonePacket);
        }
    }
}
