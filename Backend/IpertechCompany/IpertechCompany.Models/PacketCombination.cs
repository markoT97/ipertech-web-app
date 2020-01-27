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
        public Guid InternetPacketId { get; set; }
        public Guid InternetRouterId { get; set; }
        public Guid TvPacketId { get; set; }
        public Guid PhonePacketId { get; set; }

        public PacketCombination()
        {

        }

        public PacketCombination(Guid packetCombinationId, string name, Guid internetPacketId, Guid tvPacketId,
            Guid phonePacketId)
        {
            PacketCombinationId = packetCombinationId;
            Name = name;
            InternetPacketId = internetPacketId;
            TvPacketId = tvPacketId;
            PhonePacketId = phonePacketId;
        }

        public override string ToString()
        {
            return PacketCombinationId + ", " + Name + ", " + InternetPacketId + ", " + TvPacketId + ", " + PhonePacketId;
        }

        public bool IsValid()
        {
            if (!(!PacketCombinationId.Equals(null) && Name != null && !InternetPacketId.Equals(null)))
            {
                return false;
            }

            return true;
        }
    }
}
