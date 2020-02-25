using IpertechCompany.Models;
using System.ComponentModel.DataAnnotations;

namespace IpertechCompany.WebAPI.Models
{
    public class TvPacketTvChannelViewModel
    {
        [Required]
        public TvPacket TvPacket { get; set; }
        [Required]
        public TvChannel TvChannel { get; set; }
    }
}
