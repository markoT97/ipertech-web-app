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
    public class NotificationTypesController : ControllerBase
    {
        private readonly INotificationTypeRepository _notificationTypeRepository;
        private readonly INotificationTypeService _notificationTypeService;
        private readonly IMapper _mapper;

        public NotificationTypesController(IDbContext dbContext, INotificationTypeRepository notificationTypeRepository, INotificationTypeService notificationTypeService, IMapper mapper)
        {
            _notificationTypeRepository = new NotificationTypeRepository(dbContext);
            _notificationTypeService = new NotificationTypeService(_notificationTypeRepository);
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetAllNotificationTypes()
        {
            return Ok(_notificationTypeService.GetAllNotificationTypes().Select(notificationType => _mapper.Map<NotificationTypeViewModel>(notificationType)));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult InsertNotificationType(NotificationTypeViewModel notificationType)
        {
            NotificationTypeViewModel insertedNotificationType = _mapper.Map<NotificationTypeViewModel>(_mapper.Map<NotificationType>(notificationType));

            return CreatedAtAction(nameof(GetAllNotificationTypes), new { id = insertedNotificationType.NotificationTypeId }, insertedNotificationType);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult UpdateNotificationType(NotificationTypeViewModel notificationType)
        {
            _notificationTypeService.UpdateNotificationType(_mapper.Map<NotificationType>(notificationType));

            return Accepted(notificationType);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteNotificationType(Guid id)
        {
            if (!_notificationTypeService.DeleteNotificationType(id))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
