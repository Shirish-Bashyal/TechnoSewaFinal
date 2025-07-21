using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Hubs;
using Application.Hubs.InMemoryDB;
using Application.Hubs.Model;
using Application.Interfaces.Data;
using Application.Interfaces.Notification;
using Application.Response;
using Domain.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace Application.Services.Notification
{
    public class NotificationSender : INotificationSender
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly UserConnectionDb _userConnectionDb;
        private readonly NotificationDb _notificationDb;
        public readonly UserManager<ApplicationUser> _userManager;
        public readonly IUnitOfWork _uow;

        public NotificationSender(
            IHubContext<NotificationHub> hubContext,
            UserConnectionDb userConnectionDb,
            NotificationDb notificationDb,
            UserManager<ApplicationUser> userManager,
            IUnitOfWork uow
        )
        {
            _hubContext = hubContext;
            _userConnectionDb = userConnectionDb;
            _notificationDb = notificationDb;
            _uow = uow;
            _userManager = userManager;
        }

        public async Task AddNotification(NotificationDto Model, string ReceiverId)
        {
            ApplicationUser? user = await _userManager.FindByIdAsync(ReceiverId);
            if (user == null)
            {
                return;
            }

            var notification = new Domain.Entities.Notification
            {
                AddedDate = DateTime.Now,
                Message = Model.Message,
                Title = Model.Title,
                UserId = user.Id,
                User = user,
            };

            await _uow.AsyncRepositories<Domain.Entities.Notification>().AddAsync(notification);
        }

        public async Task<ServiceResponse<object>> GetNotificationByUserId(string UserId)
        {
            ApplicationUser? user = await _userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                return new ServiceResponse<object> { Message = "User not found" };
            }

            var notificationsDetails = await _uow.AsyncRepositories<Domain.Entities.Notification>()
                .GetListBySpec(x => x.UserId == UserId);
            if (notificationsDetails.Any())
            {
                var notification = notificationsDetails.Select(x => new NotificationResponse
                {
                    Message = x.Message,
                    Title = x.Title,
                    ReceivedDate = x.AddedDate
                });
                return new ServiceResponse<object> { Data = notification, Success = true, };
            }
            return new ServiceResponse<object> { Message = "no notification ", Success = true };
        }

        public async Task<bool> SendPendingNotificationsAsync(string userId, string connectionId)
        {
            var notifications = await _notificationDb.GetByUserId(userId);
            if (notifications == null || !notifications.Any())
                return false;

            foreach (var notification in notifications)
            {
                await _hubContext
                    .Clients.Client(connectionId)
                    .SendAsync("ReceiveMessage", notification);
            }

            _notificationDb.RemoveNotification(userId);

            return true;
        }

        public async Task SendToUserAsync(string userId, NotificationDto model)
        {
            var connectionId = _userConnectionDb.GetConnection(userId);

            if (string.IsNullOrEmpty(connectionId))
            {
                var notification = new NotificationDto
                {
                    Message = model.Message,
                    Title = model.Title
                };
                _notificationDb.AddNotification(userId, notification);
            }
            else
            {
                await _hubContext.Clients.Client(connectionId).SendAsync("ReceiveMessage", model);
            }
        }
    }
}
