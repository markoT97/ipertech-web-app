﻿using IpertechCompany.Models.Validation;
using System;

namespace IpertechCompany.Models
{
    public class InternetPacket : IValidation
    {
        public Guid InternetPacketId { get; set; }
        public InternetRouter InternetRouter { get; set; }
        public string Name { get; set; }
        public string Speed { get; set; }
        public decimal Price { get; set; }

        public InternetPacket()
        {
            InternetRouter = new InternetRouter();
        }

        public InternetPacket(Guid internetPacketId, InternetRouter internetRouter, string name = null, string speed = null, decimal price = 0)
        {
            InternetPacketId = internetPacketId.Equals(Guid.Empty) ? Guid.NewGuid() : internetPacketId;
            InternetRouter = internetRouter;
            Name = name;
            Speed = speed;
            Price = price;
        }

        public override string ToString()
        {
            return InternetPacketId + ", " + InternetRouter + " " + Name + ", " + Speed + ", " + Price;
        }

        public bool IsValid()
        {
            if (!(!InternetPacketId.Equals(Guid.Empty) && !InternetRouter.Equals(null) && Name != null && Speed != null &&
                  Price != 0))
            {
                return false;
            }

            return true;
        }
    }
}
