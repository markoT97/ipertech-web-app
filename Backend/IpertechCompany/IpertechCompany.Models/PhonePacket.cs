using System;
using System.Collections.Generic;
using System.Text;
using IpertechCompany.Models.Validation;

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
            PhonePacketId = phonePacketId;
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
            if (!(!PhonePacketId.Equals(null) && Name != null && FreeMinutes != 0 && Price != 0))
            {
                return false;
            }

            return true;
        }
    }
}
