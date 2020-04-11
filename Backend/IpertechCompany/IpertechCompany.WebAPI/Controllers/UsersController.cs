using AutoMapper;
using IpertechCompany.DbConnection;
using IpertechCompany.DbRepositories;
using IpertechCompany.IRepositories;
using IpertechCompany.IServices;
using IpertechCompany.Models;
using IpertechCompany.Services;
using IpertechCompany.WebAPI.Models;
using IpertechCompany.WebAPI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
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
            return Ok(_userService.GetAllUsers().Select(user => _mapper.Map<UserPasswordViewModel>(user)));
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetUserById(Guid id)
        {
            return Ok(_mapper.Map<UserViewModel>(_userService.GetByUserId(id)));
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public IActionResult LoginUser(UserLoginViewModel userLoginViewModel)
        {
            var user = _userService.GetByUserLogin(_mapper.Map<UserLogin>(userLoginViewModel));

            if (!(user != null))
            {
                return UnprocessableEntity("User name or password is incorrect");
            }
            return Ok(_userService.AuthenticateUser(_mapper.Map<User>(user)));
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult RegisterUser(UserPasswordViewModel user)
        {
            User insertedUser = _userService.CreateUser(_mapper.Map<User>(user));

            return CreatedAtAction(nameof(GetUserById), new { id = insertedUser.UserId }, insertedUser);
        }

        [Authorize(Roles = "User")]
        [HttpPut]
        public IActionResult UpdateUser(UserPasswordViewModel user)
        {
            _userService.UpdateUser(_mapper.Map<User>(user));

            return Accepted(user);
        }

        [Authorize(Roles = "User")]
        [HttpPatch]
        public IActionResult ChangeUserProfileImage([FromForm] UserImageViewModel userImage)
        {

            string fileName = userImage.UserId.ToString();
            string fileExtension = Path.GetExtension(userImage.Image.FileName);
            string imagesFolder = "wwwroot\\";
            string relativeImagePath = "account\\user-images\\" + fileName + fileExtension;
            string fullImagePath = imagesFolder + relativeImagePath;

            string imageLocationForDb = relativeImagePath.Replace("\\", "/");

            string locationPath = Path.Combine(Directory.GetCurrentDirectory(), fullImagePath);

            if (!FilesManagement.SaveFile(userImage.Image, locationPath))
            {
                return BadRequest();
            }

            _userService.UpdateUser(new UserImage(userImage.UserId, imageLocationForDb));

            return Ok(relativeImagePath.Replace("\\", "/"));
        }

        [Authorize(Roles = "User")]
        [HttpPatch]
        [Route("password")]
        public IActionResult ChangeUserPassword(UserPasswordViewModel userPassword)
        {
            _userService.UpdateUser(new UserPassword(userPassword.UserId, userPassword.Password));

            return Accepted(userPassword);
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
