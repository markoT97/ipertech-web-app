using System;
using System.Collections.Generic;
using System.Text;
using IpertechCompany.Models.Validation;

namespace IpertechCompany.Models
{
    public class InternetPacket : IValidation
    {
        public Guid InternetPacketId { get; set; }
        public Guid InternetRouterId { get; set; }
        public string Name { get; set; }
        public string Speed { get; set; }
        public decimal Price { get; set; }

        public InternetPacket()
        {
            InternetPacketId = Guid.NewGuid();
        }

        public InternetPacket(Guid internetPacketId, Guid internetRouterId, string name, string speed, decimal price)
        {
            InternetPacketId = internetPacketId;
            InternetRouterId = internetRouterId;
            Name = name;
            Speed = speed;
            Price = price;
        }

        public override string ToString()
        {
            return InternetPacketId + ", " + InternetRouterId + " " + Name + ", " + Speed + ", " + Price;
        }

        public bool IsValid()
        {
            if (!(!InternetPacketId.Equals(null) && !InternetRouterId.Equals(null) && Name != null && Speed != null &&
                  Price != 0))
            {
                return false;
            }

            return true;
        }
    }
}
