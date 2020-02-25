using System;
using System.ComponentModel.DataAnnotations;

namespace IpertechCompany.WebAPI.Models
{
    public class TvChannelViewModel
    {
        [Required]
        public Guid TvChannelId { get; set; }
        [Required]
        public string Name { get; set; }
        public string ImageLocation { get; set; }
        [Required]
        public int PositionNumber { get; set; }
        public bool TvBackwards { get; set; }
    }
}
