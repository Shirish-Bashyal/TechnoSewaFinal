using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Hubs.InMemoryDB;
using Application.Hubs.Model;
using Application.Interfaces.Notification;
using Domain.Entities.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly INotificationSender _notificationSender;
        private readonly UserConnectionDb _userConnectionDb;

        public NotificationHub(
            INotificationSender notificationSender,
            UserConnectionDb userConnectionDb
        )
        {
            _notificationSender = notificationSender;
            _userConnectionDb = userConnectionDb;
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return;

            _userConnectionDb.AddConnection(userId, Context.ConnectionId);

            await _notificationSender.SendPendingNotificationsAsync(userId, Context.ConnectionId);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            _userConnectionDb.RemoveConnection(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
