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
    public class MessagesController : ControllerBase
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        public MessagesController(IDbContext dbContext, IMessageRepository messageRepository, IMessageService messageService, IMapper mapper)
        {
            _messageRepository = new MessageRepository(dbContext);
            _messageService = new MessageService(_messageRepository);
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetMessageById(Guid id)
        {
            return Ok(_mapper.Map<MessageViewModel>(_messageService.GetByMessageId(id)));
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult InsertMessage(MessageViewModel message)
        {
            MessageViewModel insertedMessage = _mapper.Map<MessageViewModel>(_messageService.CreateMessage(_mapper.Map<Message>(message)));

            return CreatedAtAction(nameof(GetMessageById), new { id = insertedMessage.MessageId }, insertedMessage);
        }

        [Authorize(Roles = "User")]
        public IActionResult UpdateMessage(MessageViewModel message)
        {
            _messageService.UpdateMessage(_mapper.Map<Message>(message));

            return Accepted(message);
        }

        [Authorize(Roles = "User")]
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteMessage(Guid id)
        {
            if (!_messageService.DeleteMessage(id))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
