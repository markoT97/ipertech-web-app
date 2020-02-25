using IpertechCompany.Models.Validation;
using System;

namespace IpertechCompany.Models
{
    public class TvPacketTvChannel : IValidation
    {
        public TvPacket TvPacket { get; set; }
        public TvChannel TvChannel { get; set; }

        public TvPacketTvChannel()
        {
            TvPacket = new TvPacket();
            TvChannel = new TvChannel();
        }

        public TvPacketTvChannel(TvPacket tvPacket, TvChannel tvChannel)
        {
            TvPacket = tvPacket;
            TvChannel = tvChannel;
        }

        public override string ToString()
        {
            return TvPacket + ", " + TvChannel;
        }

        public bool IsValid()
        {
            if (!(!TvPacket.TvPacketId.Equals(Guid.Empty) && !TvChannel.TvChannelId.Equals(Guid.Empty)))
            {
                return false;
            }

            return true;
        }
    }
}
