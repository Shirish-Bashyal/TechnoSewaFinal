using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Hubs.InMemoryDB;
using Application.Hubs.Model;
using Domain.Entities.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly UserConnectionDb _sharedDb;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly NotificationDb _notificationDb;

        public NotificationHub(
            UserConnectionDb sharedDb,
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager,
            NotificationDb notificationDb
        )
        {
            _sharedDb = sharedDb;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _notificationDb = notificationDb;
        }

        public override async Task OnConnectedAsync()
        {
            var userId = _httpContextAccessor
                .HttpContext?.User
                ?.FindFirst(ClaimTypes.NameIdentifier)
                ?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return;
            }

            var connectionId = Context.ConnectionId;

            _sharedDb.AddConnection(userId, connectionId);

            var sendNotification = await SendPendingNotification(userId, connectionId);
            if (sendNotification == true)
            {
                _notificationDb.RemoveNotification(userId);
            }
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = _httpContextAccessor
                .HttpContext?.User
                ?.FindFirst(ClaimTypes.NameIdentifier)
                ?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return;
            }

            var connection = _sharedDb.GetConnection(userId);
            if (connection != null)
            {
                _sharedDb.RemoveConnection(userId);
            }
        }

        public async Task<bool> SendPendingNotification(string userId, string connectionId)
        {
            if (userId != null)
            {
                var notifications = await _notificationDb.GetByUserId(userId);
                if (notifications != null && notifications.Any())
                {
                    foreach (var notification in notifications)
                    {
                        string messages = notification.Message;

                        await Clients.Client(connectionId).SendAsync("ReceiveMessage", messages);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public async Task SendNotificationToUser(string message, string receiverId)
        {
            var connectionId = _sharedDb.GetConnection(receiverId);
            if (connectionId == null)
            {
                var notification = new NotificationDto() { Message = message, };

                _notificationDb.AddNotification(receiverId, notification);
            }
            else
            {
                await Clients.Client(connectionId).SendAsync("ReceiveMessage", message);
            }
        }
    }
}
