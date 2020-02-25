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

namespace IpertechCompany.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollsController : ControllerBase
    {
        private readonly IPollRepository _pollRepository;
        private readonly IPollService _pollService;
        private readonly IMapper _mapper;

        public PollsController(IDbContext dbContext, IPollRepository pollRepository, IPollService pollService, IMapper mapper)
        {
            _pollRepository = new PollRepository(dbContext);
            _pollService = new PollService(_pollRepository);
            _mapper = mapper;
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetPollById(Guid id)
        {
            return Ok(_mapper.Map<PollViewModel>(_pollService.GetByPollId(id)));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult InsertPoll(PollViewModel poll)
        {
            PollViewModel insertedPoll = _mapper.Map<PollViewModel>(_pollService.CreatePoll(_mapper.Map<Poll>(poll)));

            return CreatedAtAction(nameof(GetPollById), new { id = insertedPoll.PollId }, insertedPoll);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult UpdatePoll(PollViewModel poll)
        {
            _pollService.UpdatePoll(_mapper.Map<Poll>(poll));

            return Accepted(poll);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeletePoll(Guid id)
        {
            if (!_pollService.DeletePoll(id))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
