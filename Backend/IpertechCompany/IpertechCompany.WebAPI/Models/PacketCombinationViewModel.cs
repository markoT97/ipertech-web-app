using IpertechCompany.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace IpertechCompany.WebAPI.Models
{
    public class PacketCombinationViewModel
    {
        [Required]
        public Guid PacketCombinationId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public InternetPacket InternetPacket { get; set; }
        public TvPacket TvPacket { get; set; }
        public PhonePacket PhonePacket { get; set; }
    }
}
