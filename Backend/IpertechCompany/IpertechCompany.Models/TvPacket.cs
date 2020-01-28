using System;
using System.Collections.Generic;
using System.Text;
using IpertechCompany.Models.Validation;

namespace IpertechCompany.Models
{
    public class TvPacket : IValidation
    {
        public Guid TvPacketId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public TvPacket()
        {
            TvPacketId = Guid.NewGuid();
        }

        public TvPacket(Guid tvPacketId, string name, decimal price)
        {
            TvPacketId = tvPacketId;
            Name = name;
            Price = price;
        }

        public override string ToString()
        {
            return TvPacketId + ", " + Name + ", " + Price;
        }

        public bool IsValid()
        {
            if (!(!TvPacketId.Equals(null) && Name != null && Price != 0))
            {
                return false;
            }

            return true;
        }
    }
}
