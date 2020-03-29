using IpertechCompany.Models.Validation;
using System;

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
            InternetPacket = new InternetPacket();
            TvPacket = new TvPacket();
            PhonePacket = new PhonePacket();
        }

        public PacketCombination(Guid packetCombinationId, InternetPacket internetPacket = null, string name = null, TvPacket tvPacket = null,
            PhonePacket phonePacket = null)
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
            if (!(!PacketCombinationId.Equals(Guid.Empty) && Name != null && !InternetPacket.Equals(null)))
            {
                return false;
            }

            return true;
        }
    }
}
