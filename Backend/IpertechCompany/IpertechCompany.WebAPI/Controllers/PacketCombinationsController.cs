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
    public class PacketCombinationsController : ControllerBase
    {
        private readonly IPacketCombinationRepository _packetCombinationRepository;
        private readonly IPacketCombinationService _packetCombinationService;
        private readonly IMapper _mapper;

        public PacketCombinationsController(IDbContext dbContext, IPacketCombinationRepository packetCombinationRepository, IPacketCombinationService packetCombinationService, IMapper mapper)
        {
            _packetCombinationRepository = new PacketCombinationRepository(dbContext);
            _packetCombinationService = new PacketCombinationService(_packetCombinationRepository);
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAllPacketCombinations()
        {
            return Ok(_packetCombinationService.GetAllPacketCombinations().Select(packetCombination => _mapper.Map<PacketCombinationViewModel>(packetCombination)));
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        [Route("ids")]
        public IActionResult
        GetPacketCombinationByInternetAndTvAndPhonePacketId([FromQuery]Guid internetPacketId, [FromQuery]Guid? tvPacketId = null, [FromQuery] Guid? phonePacketId = null)
        {
            return Ok(_mapper.Map<PacketCombinationViewModel>(_packetCombinationService.GetPacketCombinationByInternetAndTvAndPhonePacketId(internetPacketId, tvPacketId, phonePacketId)));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult InsertPacketCombination(PacketCombinationViewModel packetCombination)
        {
            PacketCombinationViewModel insertedPacketCombination = _mapper.Map<PacketCombinationViewModel>(_packetCombinationService.CreatePacketCombination(_mapper.Map<PacketCombination>(packetCombination)));

            return CreatedAtAction(nameof(GetAllPacketCombinations), new { id = insertedPacketCombination.PacketCombinationId }, insertedPacketCombination);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult UpdatePacketCombination(PacketCombinationViewModel packetCombination)
        {
            _packetCombinationService.UpdatePacketCombination(_mapper.Map<PacketCombination>(packetCombination));

            return Accepted(packetCombination);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeletePacketCombination(Guid id)
        {
            if (!_packetCombinationService.DeletePacketCombination(id))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
