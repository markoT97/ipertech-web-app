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
    public class InternetPacketsController : ControllerBase
    {
        private readonly IInternetPacketRepository _internetPacketRepository;
        private readonly IInternetPacketService _internetPacketService;
        private readonly IMapper _mapper;

        public InternetPacketsController(IDbContext dbContext, IInternetPacketRepository internetPacketRepository, IInternetPacketService internetPacketService, IMapper mapper)
        {
            _internetPacketRepository = new InternetPacketRepository(dbContext);
            _internetPacketService = new InternetPacketService(_internetPacketRepository);
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAllInternetPackets()
        {
            return Ok(_internetPacketService.GetAllInternetPackets().Select(internetPacket => _mapper.Map<InternetPacketViewModel>(internetPacket)));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult InsertInternetPacket(InternetPacketViewModel internetPacket)
        {
            InternetPacketViewModel insertedInternetPacket = _mapper.Map<InternetPacketViewModel>(_internetPacketService.CreateInternetPacket(_mapper.Map<InternetPacket>(internetPacket)));

            return CreatedAtAction(nameof(GetAllInternetPackets), new { id = insertedInternetPacket.InternetPacketId }, insertedInternetPacket);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult UpdateInternetPacket(InternetPacketViewModel internetPacket)
        {
            _internetPacketService.UpdateInternetPacket(_mapper.Map<InternetPacket>(internetPacket));

            return Accepted(internetPacket);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{internetPacketId}/{internetRouterId}")]
        public IActionResult DeleteInternetPacket(Guid internetPacketId, Guid internetRouterId)
        {
            if (!_internetPacketService.DeleteInternetPacket(internetPacketId, internetRouterId))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
