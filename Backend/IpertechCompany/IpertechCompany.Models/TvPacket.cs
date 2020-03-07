﻿using IpertechCompany.Models.Validation;
using System;
using System.Collections.Generic;

namespace IpertechCompany.Models
{
    public class TvPacket : IValidation
    {
        public Guid TvPacketId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public List<TvChannel> TvChannels { get; set; }

        public TvPacket()
        {
            TvChannels = new List<TvChannel>();
        }

        public TvPacket(Guid tvPacketId, string name = null, decimal price = 0)
        {
            TvPacketId = tvPacketId.Equals(Guid.Empty) ? Guid.NewGuid() : tvPacketId;
            Name = name;
            Price = price;
        }

        public override string ToString()
        {
            return TvPacketId + ", " + Name + ", " + Price;
        }

        public bool IsValid()
        {
            if (!(!TvPacketId.Equals(Guid.Empty) && Name != null && Price != 0))
            {
                return false;
            }

            return true;
        }
    }
}
