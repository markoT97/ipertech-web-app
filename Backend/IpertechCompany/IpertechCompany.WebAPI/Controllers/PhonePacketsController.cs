using AutoMapper;
using IpertechCompany.DbConnection;
using IpertechCompany.DbRepositories;
using IpertechCompany.IRepositories;
using IpertechCompany.IServices;
using IpertechCompany.Models;
using IpertechCompany.Services;
using IpertechCompany.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace IpertechCompany.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhonePacketsController : ControllerBase
    {
        private readonly IPhonePacketRepository _phonePacketRepository;
        private readonly IPhonePacketService _phonePacketService;
        private readonly IMapper _mapper;

        public PhonePacketsController(IDbContext dbContext, IPhonePacketRepository phonePacketRepository, IPhonePacketService phonePacketService, IMapper mapper)
        {
            _phonePacketRepository = new PhonePacketRepository(dbContext);
            _phonePacketService = new PhonePacketService(_phonePacketRepository);
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAllPhonePackets()
        {
            return Ok(_phonePacketService.GetAllPhonePackets().Select(phonePacket => _mapper.Map<PhonePacketViewModel>(phonePacket)));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult InsertPhonePacket(PhonePacketViewModel phonePacket)
        {
            PhonePacketViewModel insertedPhonePacket = _mapper.Map<PhonePacketViewModel>(_phonePacketService.CreatePhonePacket(_mapper.Map<PhonePacket>(phonePacket)));

            return CreatedAtAction(nameof(GetAllPhonePackets), new { id = insertedPhonePacket.PhonePacketId }, insertedPhonePacket);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult UpdatePhonePacket(PhonePacketViewModel phonePacket)
        {
            _phonePacketService.UpdatePhonePacket(_mapper.Map<PhonePacket>(phonePacket));

            return Accepted(phonePacket);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeletePhonePacket(Guid id)
        {
            if (!_phonePacketService.DeletePhonePacket(id))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
