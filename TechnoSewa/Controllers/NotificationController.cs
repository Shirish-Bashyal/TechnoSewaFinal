using System.Security.Claims;
using Application.Interfaces.Notification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TechnoSewa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationSender _notificationSender;

        public NotificationController(INotificationSender notificationSender)
        {
            _notificationSender = notificationSender;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllNotification()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                var result = await _notificationSender.GetNotificationByUserId(userId);
                if (result.Success)
                {
                    return StatusCode(200, result);
                }
                else
                {
                    return StatusCode(500, result);
                }
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
