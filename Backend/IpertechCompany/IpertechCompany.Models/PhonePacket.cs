using IpertechCompany.Models.Validation;
using System;

namespace IpertechCompany.Models
{
    public class PhonePacket : IValidation
    {
        public Guid PhonePacketId { get; set; }
        public string Name { get; set; }
        public int FreeMinutes { get; set; }
        public decimal Price { get; set; }

        public PhonePacket()
        {

        }

        public PhonePacket(Guid phonePacketId, string name, int freeMinutes, decimal price)
        {
            PhonePacketId = phonePacketId.Equals(Guid.Empty) ? Guid.NewGuid() : phonePacketId;
            Name = name;
            FreeMinutes = freeMinutes;
            Price = price;
        }

        public override string ToString()
        {
            return PhonePacketId + ", " + Name + ", " + FreeMinutes;
        }

        public bool IsValid()
        {
            if (!(!PhonePacketId.Equals(Guid.Empty) && Name != null && FreeMinutes >= 0 && Price != 0))
            {
                return false;
            }

            return true;
        }
    }
}
