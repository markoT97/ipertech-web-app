using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IpertechCompany.WebAPI.Models
{
    public class TvPacketViewModel
    {
        [Required]
        public Guid TvPacketId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }

        public List<TvChannelViewModel> TvChannels { get; set; }
    }
}
