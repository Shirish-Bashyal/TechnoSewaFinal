using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Hubs.Model;
using Application.Response;

namespace Application.Interfaces.Notification
{
    public interface INotificationSender
    {
        Task SendToUserAsync(string userId, NotificationDto model);
        Task<bool> SendPendingNotificationsAsync(string userId, string connectionId);
        Task<ServiceResponse<object>> GetNotificationByUserId(string UserId);
        Task AddNotification(NotificationDto Model, string ReceiverId);
    }
}
