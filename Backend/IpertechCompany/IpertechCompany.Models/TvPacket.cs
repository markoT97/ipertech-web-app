using IpertechCompany.Models.Validation;
using System;

namespace IpertechCompany.Models
{
    public class TvPacket : IValidation
    {
        public Guid TvPacketId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public TvPacket()
        {

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
            if (!(!TvPacketId.Equals(null) && Name != null && Price != 0))
            {
                return false;
            }

            return true;
        }
    }
}
