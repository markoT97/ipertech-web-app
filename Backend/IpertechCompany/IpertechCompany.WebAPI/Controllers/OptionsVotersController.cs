﻿using AutoMapper;
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
    public class OptionsVotersController : ControllerBase
    {
        private readonly IOptionVoterRepository _optionVoterRepository;
        private readonly IOptionVoterService _optionVoterService;
        private readonly IMapper _mapper;

        public OptionsVotersController(IDbContext dbContext, IOptionVoterRepository optionVoterRepository, IOptionVoterService optionVoterService, IMapper mapper)
        {
            _optionVoterRepository = new OptionVoterRepository(dbContext);
            _optionVoterService = new OptionVoterService(_optionVoterRepository);
            _mapper = mapper;
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetNumberOfVotersByPollOptionId(Guid id)
        {
            return Ok(_optionVoterService.GetNumberOfVotersByPollOptionId(id));
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult InsertOptionVoter(OptionVoterViewModel optionVoter)
        {
            OptionVoterViewModel insertedOptionVoter = _mapper.Map<OptionVoterViewModel>(_optionVoterService.CreateOptionVoter(_mapper.Map<OptionVoter>(optionVoter)));

            return CreatedAtAction(nameof(GetNumberOfVotersByPollOptionId), new { id = insertedOptionVoter.PollOptionId }, insertedOptionVoter);
        }
    }
}
