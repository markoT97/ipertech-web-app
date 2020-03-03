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
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public NotificationsController(IDbContext dbContext, INotificationRepository notificationRepository, INotificationService notificationService, IMapper mapper)
        {
            _notificationRepository = new NotificationRepository(dbContext);
            _notificationService = new NotificationService(_notificationRepository);
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAllNotifications()
        {
            return Ok(_notificationService.GetAllNotifications().Select(notification => _mapper.Map<NotificationViewModel>(notification)));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{numberOfNewestNotifications}")]
        public IActionResult GetNewestNotifications(int numberOfNewestNotifications)
        {
            return Ok(_notificationService.GetAllNotifications(numberOfNewestNotifications).Select(notification => _mapper.Map<NotificationViewModel>(notification)));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{id}/notificationTypes/{numberOfNewestNotifications}")]
        public IActionResult GetNotificationsByNotificationTypeId(Guid id, int numberOfNewestNotifications)
        {
            return Ok(_notificationService.GetByNotificationTypeId(id, numberOfNewestNotifications).Select(notification => _mapper.Map<NotificationViewModel>(notification)));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult InsertNotification(NotificationViewModel notification)
        {
            NotificationViewModel insertedNotification = _mapper.Map<NotificationViewModel>(_notificationService.CreateNotification(_mapper.Map<Notification>(notification)));

            return CreatedAtAction(nameof(GetNotificationsByNotificationTypeId), new { id = insertedNotification.NotificationId }, insertedNotification);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult UpdateNotification(NotificationViewModel notification)
        {
            _notificationService.UpdateNotification(_mapper.Map<Notification>(notification));

            return Accepted(notification);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{notificationId}/{notificationTypeId}")]
        public IActionResult DeleteNotification(Guid notificationId, Guid notificationTypeId)
        {
            if (!_notificationService.DeleteNotification(notificationId, notificationTypeId))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
