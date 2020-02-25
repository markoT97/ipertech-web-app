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
    public class PollOptionsController : ControllerBase
    {
        private readonly IPollOptionRepository _pollOptionRepository;
        private readonly IPollOptionService _pollOptionService;
        private readonly IMapper _mapper;

        public PollOptionsController(IDbContext dbContext, IPollOptionRepository pollOptionRepository, IPollOptionService pollOptionService, IMapper mapper)
        {
            _pollOptionRepository = new PollOptionRepository(dbContext);
            _pollOptionService = new PollOptionService(_pollOptionRepository);
            _mapper = mapper;
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetPollOptionByPollId(Guid id)
        {
            return Ok(_pollOptionService.GetByPollId(id).Select(pollOption => _mapper.Map<PollOptionViewModel>(pollOption)));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult InsertPollOption(PollOptionViewModel pollOption)
        {
            PollOptionViewModel insertedPollOption = _mapper.Map<PollOptionViewModel>(_pollOptionService.CreatePollOption(_mapper.Map<PollOption>(pollOption)));

            return CreatedAtAction(nameof(GetPollOptionByPollId), new { id = insertedPollOption.Poll.PollId }, insertedPollOption);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult UpdatePollOption(PollOptionViewModel pollOption)
        {
            _pollOptionService.UpdatePollOption(_mapper.Map<PollOption>(pollOption));

            return Accepted(pollOption);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeletePollOption(Guid id)
        {
            if (!_pollOptionService.DeletePollOption(id))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
