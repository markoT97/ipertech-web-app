using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using IpertechCompany.Models.Validation;

namespace IpertechCompany.Models
{
    class TvChannel : IValidation
    {
        public Guid TvChannelId { get; set; }
        public string Name { get; set; }
        public string ImageLocation { get; set; }
        public int PositionNumber { get; set; }
        public bool TvBackwards { get; set; }

        public TvChannel()
        {

        }

        public TvChannel(Guid tvChannelId, string name, string imageLocation, int positionNumber, bool tvBackwards)
        {
            TvChannelId = tvChannelId;
            Name = name;
            ImageLocation = imageLocation;
            PositionNumber = positionNumber;
            TvBackwards = tvBackwards;
        }

        public override string ToString()
        {
            return TvChannelId + ", " + Name + ", " + ImageLocation + ", " + PositionNumber + ", " + TvBackwards;
        }

        public bool IsValid()
        {
            if (!(!TvChannelId.Equals(null) && Name != null && PositionNumber != 0))
            {
                return false;
            }

            return true;
        }
    }
}
