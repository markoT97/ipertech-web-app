using System;
using System.Collections.Generic;
using System.Text;
using IpertechCompany.Models.Validation;

namespace IpertechCompany.Models
{
    public class TvPacketTvChannel : IValidation
    {
        public TvPacket TvPacket { get; set; }
        public TvChannel TvChannel { get; set; }

        public TvPacketTvChannel()
        {

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
            if (!(!TvPacket.Equals(null) && !TvChannel.Equals(null)))
            {
                return false;
            }

            return true;
        }
    }
}
