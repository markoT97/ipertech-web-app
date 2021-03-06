﻿using IpertechCompany.Models;
using System;
using System.Collections.Generic;

namespace IpertechCompany.IRepositories
{
    public interface IPacketCombinationRepository
    {
        IEnumerable<PacketCombination> GetAll();
        PacketCombination Get(Guid internetPacketId, Guid? tvPacketId, Guid? phonePacketId);
        PacketCombination Insert(PacketCombination packetCombination);
        void Update(PacketCombination packetCombination);
        bool Delete(Guid packetCombinationId);
    }
}
