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
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace IpertechCompany.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IDbContext dbContext, IUserRepository userRepository, IUserService userService, IConfiguration configuration, IMapper mapper)
        {
            _userRepository = new UserRepository(dbContext);
            _userService = new UserService(_userRepository, configuration);
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetAllUser()
        {
            return Ok(_userService.GetAllUsers().Select(user => _mapper.Map<UserViewModel>(user)));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetUserById(Guid id)
        {
            return Ok(_mapper.Map<UserViewModel>(_userService.GetByUserId(id)));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("login")]
        public IActionResult LoginUser(UserLoginViewModel userLoginViewModel)
        {
            return Ok(_userService.LoginUser(_mapper.Map<UserLogin>(userLoginViewModel)));
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult RegisterUser(UserViewModel user)
        {
            User insertedUser = _userService.CreateUser(_mapper.Map<User>(user));

            return CreatedAtAction(nameof(GetUserById), new { id = insertedUser.UserId }, insertedUser);
        }

        [Authorize(Roles = "User")]
        [HttpPut]
        public IActionResult UpdateUser(UserViewModel user)
        {
            _userService.UpdateUser(_mapper.Map<User>(user));

            return Accepted(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteUser(Guid id)
        {
            if (!_userService.DeleteUser(id))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
