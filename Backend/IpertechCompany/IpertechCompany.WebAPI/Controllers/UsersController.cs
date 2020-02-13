using AutoMapper;
using IpertechCompany.DbConnection;
using IpertechCompany.DbRepositories;
using IpertechCompany.IRepositories;
using IpertechCompany.IServices;
using IpertechCompany.Models;
using IpertechCompany.Services;
using IpertechCompany.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
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

        public UsersController(IDbContext dbContext, IUserRepository userRepository, IUserService userService, IMapper mapper)
        {
            _userRepository = new UserRepository(dbContext);
            _userService = new UserService(_userRepository);
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllUser()
        {
            return Ok(_userService.GetAllUsers().Select(user => _mapper.Map<UserViewModel>(user)));
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetUserById(Guid id)
        {
            return Ok(_userService.GetByUserId(id));
        }

        [HttpGet]
        [Route("login")]
        public IActionResult LoginUser(UserLoginViewModel userLoginViewModel)
        {
            return Ok(_userService.LoginUser(_mapper.Map<UserLogin>(userLoginViewModel)));
        }

        [HttpPost]
        public IActionResult RegisterUser(UserViewModel user)
        {
            User insertedUser = _userService.CreateUser(_mapper.Map<User>(user));

            return CreatedAtAction(nameof(GetUserById), new { id = insertedUser.UserId }, insertedUser);
        }

        [HttpPut]
        public IActionResult UpdateUser(UserViewModel user)
        {
            _userService.UpdateUser(_mapper.Map<User>(user));

            return Accepted(user);
        }

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
