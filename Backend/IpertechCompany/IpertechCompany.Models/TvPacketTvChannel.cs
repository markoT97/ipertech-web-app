using System;
using System.Collections.Generic;
using System.Text;
using IpertechCompany.Models.Validation;

namespace IpertechCompany.Models
{
    public class TvPacketTvChannel : IValidation
    {
        public Guid TvPacketId { get; set; }
        public Guid TvChannelId { get; set; }

        public TvPacketTvChannel()
        {

        }

        public TvPacketTvChannel(Guid tvPacketId, Guid tvChannelId)
        {
            TvPacketId = tvPacketId;
            TvChannelId = tvChannelId;
        }

        public override string ToString()
        {
            return TvPacketId + ", " + TvChannelId;
        }

        public bool IsValid()
        {
            if (!(!TvPacketId.Equals(null) && !TvChannelId.Equals(null)))
            {
                return false;
            }

            return true;
        }
    }
}
