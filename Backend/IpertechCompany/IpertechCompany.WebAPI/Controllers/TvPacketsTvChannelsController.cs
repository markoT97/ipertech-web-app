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
    public class TvPacketsTvChannelsController : ControllerBase
    {
        private readonly ITvPacketTvChannelRepository _tvPacketTvChannelRepository;
        private readonly ITvPacketTvChannelService _tvPacketTvChannelService;
        private readonly IMapper _mapper;

        public TvPacketsTvChannelsController(IDbContext dbContext, ITvPacketTvChannelRepository tvPacketTvChannelRepository, ITvPacketTvChannelService tvPacketTvChannelService, IMapper mapper)
        {
            _tvPacketTvChannelRepository = new TvPacketTvChannelRepository(dbContext);
            _tvPacketTvChannelService = new TvPacketTvChannelService(_tvPacketTvChannelRepository);
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetTvChannelsByTvPacketId(Guid id)
        {
            return Ok(_tvPacketTvChannelService.GetTvChannelsByTvPacketId(id).Select(tvPacketTvChannel => _mapper.Map<TvChannelViewModel>(tvPacketTvChannel)));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult InsertTvPacketTvChannel(TvPacketTvChannelViewModel tvPacketTvChannel)
        {
            TvPacketTvChannelViewModel insertedTvPacketTvChannel = _mapper.Map<TvPacketTvChannelViewModel>(_tvPacketTvChannelService.CreateTvPacketTvChannel(_mapper.Map<TvPacketTvChannel>(tvPacketTvChannel)));

            return CreatedAtAction(nameof(GetTvChannelsByTvPacketId), new { id = insertedTvPacketTvChannel.TvPacket.TvPacketId }, insertedTvPacketTvChannel);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{tvPacketId}/{tvChannelId}")]
        public IActionResult DeleteTvPacketTvChannel(Guid tvPacketId, Guid tvChannelId)
        {
            if (!_tvPacketTvChannelService.DeleteTvPacketTvChannel(new TvPacketTvChannel(new TvPacket(tvPacketId), new TvChannel(tvChannelId))))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
