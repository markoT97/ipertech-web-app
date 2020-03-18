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
    public class UserMessagesController : ControllerBase
    {
        private readonly IUserMessageRepository _userMessageRepository;
        private readonly IUserMessageService _userMessageService;
        private readonly IMapper _mapper;

        public UserMessagesController(IDbContext dbContext, IUserMessageRepository userMessageRepository, IUserMessageService userMessageService, IMapper mapper)
        {
            _userMessageRepository = new UserMessageRepository(dbContext);
            _userMessageService = new UserMessageService(_userMessageRepository);
            _mapper = mapper;
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        [Route("{id}/{offset}/{numberOfRows}")]
        public IActionResult GetMessagesByUserId(Guid id, int offset, int numberOfRows)
        {
            return Ok(_userMessageService.GetMessagesByUserId(id, offset, numberOfRows).Select(message => _mapper.Map<MessageViewModel>(message)));
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetTotalNumberOfMessagesByUserId(Guid id)
        {
            return Ok(_userMessageService.GetMessagesByUserId(id, 0, 10).Select(message => _mapper.Map<MessageViewModel>(message)));
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult InsertUserMessage(UserMessageViewModel userMessage)
        {
            UserMessageViewModel insertedUserMessage = _mapper.Map<UserMessageViewModel>(_userMessageService.CreateUserMessage(_mapper.Map<UserMessage>(userMessage)));

            return CreatedAtAction(nameof(GetMessagesByUserId), new { id = insertedUserMessage.User.UserId }, insertedUserMessage);
        }

        [Authorize(Roles = "User")]
        [HttpDelete]
        [Route("{userId}/{messageId}")]
        public IActionResult DeleteUserMessage(Guid userId, Guid messageId)
        {
            if (!_userMessageService.DeleteUserMessage(new UserMessage(new User(userId), new Message(messageId))))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
