using System;
using System.Collections.Generic;
using System.Text;
using IpertechCompany.Models.Validation;

namespace IpertechCompany.Models
{
    public class PacketCombination : IValidation
    {
        public Guid PacketCombinationId { get; set; }
        public string Name { get; set; }
        public InternetPacket InternetPacket { get; set; }
        public TvPacket TvPacket { get; set; }
        public PhonePacket PhonePacket { get; set; }

        public PacketCombination()
        {
            
        }

        public PacketCombination(Guid packetCombinationId, string name, InternetPacket internetPacket, TvPacket tvPacket,
            PhonePacket phonePacket)
        {
            PacketCombinationId = packetCombinationId.Equals(Guid.Empty) ? Guid.NewGuid() : packetCombinationId;
            Name = name;
            InternetPacket = internetPacket;
            TvPacket = tvPacket;
            PhonePacket = phonePacket;
        }

        public override string ToString()
        {
            return PacketCombinationId + ", " + Name + ", " + InternetPacket + ", " + TvPacket + ", " + PhonePacket;
        }

        public bool IsValid()
        {
            if (!(!PacketCombinationId.Equals(null) && Name != null && !InternetPacket.Equals(null)))
            {
                return false;
            }

            return true;
        }
    }
}
