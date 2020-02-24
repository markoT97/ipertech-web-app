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
    public class InternetRoutersController : ControllerBase
    {
        private readonly IInternetRouterRepository _internetRouterRepository;
        private readonly IInternetRouterService _internetRouterService;
        private readonly IMapper _mapper;

        public InternetRoutersController(IDbContext dbContext, IInternetRouterRepository internetRouterRepository, IInternetRouterService internetRouterService, IMapper mapper)
        {
            _internetRouterRepository = new InternetRouterRepository(dbContext);
            _internetRouterService = new InternetRouterService(_internetRouterRepository);
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetInternetRouterById(Guid id)
        {
            return Ok(_mapper.Map<InternetRouterViewModel>(_internetRouterService.GetByInternetRouterId(id)));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult InsertInternetRouter(InternetRouterViewModel internetRouter)
        {
            InternetRouterViewModel insertedInternetRouter = _mapper.Map<InternetRouterViewModel>(_internetRouterService.CreateInternetRouter(_mapper.Map<InternetRouter>(internetRouter)));

            return CreatedAtAction(nameof(GetInternetRouterById), new { id = insertedInternetRouter.InternetRouterId }, insertedInternetRouter);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult UpdateInternetRouter(InternetRouterViewModel internetRouter)
        {
            _internetRouterService.UpdateInternetRouter(_mapper.Map<InternetRouter>(internetRouter));

            return Accepted(internetRouter);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteInternetRouter(Guid id)
        {
            if (!_internetRouterService.DeleteInternetRouter(id))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
