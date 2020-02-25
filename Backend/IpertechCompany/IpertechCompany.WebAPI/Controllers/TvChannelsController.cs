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
    public class TvChannelsController : ControllerBase
    {
        private readonly ITvChannelRepository _tvChannelRepository;
        private readonly ITvChannelService _tvChannelService;
        private readonly IMapper _mapper;

        public TvChannelsController(IDbContext dbContext, ITvChannelRepository tvChannelRepository, ITvChannelService tvChannelService, IMapper mapper)
        {
            _tvChannelRepository = new TvChannelRepository(dbContext);
            _tvChannelService = new TvChannelService(_tvChannelRepository);
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAllTvChannels()
        {
            return Ok(_tvChannelService.GetAllTvChannels().Select(tvChannel => _mapper.Map<TvChannelViewModel>(tvChannel)));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult InsertTvChannel(TvChannelViewModel tvChannel)
        {
            TvChannelViewModel insertedTvChannel = _mapper.Map<TvChannelViewModel>(_tvChannelService.CreateTvChannel(_mapper.Map<TvChannel>(tvChannel)));

            return CreatedAtAction(nameof(GetAllTvChannels), new { id = insertedTvChannel.TvChannelId }, insertedTvChannel);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult UpdateTvChannel(TvChannelViewModel tvChannel)
        {
            _tvChannelService.UpdateTvChannel(_mapper.Map<TvChannel>(tvChannel));

            return Accepted(tvChannel);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteTvChannel(Guid id)
        {
            if (!_tvChannelService.DeleteTvChannel(id))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
