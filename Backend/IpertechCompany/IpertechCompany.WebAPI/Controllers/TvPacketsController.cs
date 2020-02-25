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
    public class TvPacketsController : ControllerBase
    {
        private readonly ITvPacketRepository _tvPacketRepository;
        private readonly ITvPacketService _tvPacketService;
        private readonly IMapper _mapper;

        public TvPacketsController(IDbContext dbContext, ITvPacketRepository tvPacketRepository, ITvPacketService tvPacketService, IMapper mapper)
        {
            _tvPacketRepository = new TvPacketRepository(dbContext);
            _tvPacketService = new TvPacketService(_tvPacketRepository);
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAllTvPackets()
        {
            return Ok(_tvPacketService.GetAllTvPackets().Select(tvPacket => _mapper.Map<TvPacketViewModel>(tvPacket)));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult InsertTvPacket(TvPacketViewModel tvPacket)
        {
            TvPacketViewModel insertedTvPacket = _mapper.Map<TvPacketViewModel>(_tvPacketService.CreateTvPacket(_mapper.Map<TvPacket>(tvPacket)));

            return CreatedAtAction(nameof(GetAllTvPackets), new { id = insertedTvPacket.TvPacketId }, insertedTvPacket);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult UpdateTvPacket(TvPacketViewModel tvPacket)
        {
            _tvPacketService.UpdateTvPacket(_mapper.Map<TvPacket>(tvPacket));

            return Accepted(tvPacket);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteTvPacket(Guid id)
        {
            if (!_tvPacketService.DeleteTvPacket(id))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
