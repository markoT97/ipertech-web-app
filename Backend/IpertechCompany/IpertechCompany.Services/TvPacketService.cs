using IpertechCompany.IRepositories;
using IpertechCompany.IServices;
using IpertechCompany.Models;
using System;
using System.Collections.Generic;

namespace IpertechCompany.Services
{
    public class TvPacketService : ITvPacketService
    {
        private readonly ITvPacketRepository _tvPacketRepository;

        public TvPacketService(ITvPacketRepository tvPacket)
        {
            _tvPacketRepository = tvPacket;
        }

        public TvPacket CreateTvPacket(TvPacket tvPacket)
        {
            if (!(tvPacket != null))
            {
                throw new ArgumentNullException("tvPacket", "Parameter is null.");
            }

            if (!tvPacket.IsValid())
            {
                throw new ArgumentException("Missing required properties.");
            }

            return _tvPacketRepository.Insert(tvPacket);
        }

        public bool DeleteTvPacket(Guid tvPacketId)
        {
            return _tvPacketRepository.Delete(tvPacketId);
        }

        public IEnumerable<TvPacket> GetAllTvPackets()
        {
            return _tvPacketRepository.GetAll();
        }

        public void UpdateTvPacket(TvPacket tvPacket)
        {
            if (!(tvPacket != null))
            {
                throw new ArgumentNullException("tvPacket", "Parameter is null.");
            }

            if (!tvPacket.IsValid())
            {
                throw new ArgumentException("Missing required properties.");
            }
            _tvPacketRepository.Update(tvPacket);
        }
    }
}
