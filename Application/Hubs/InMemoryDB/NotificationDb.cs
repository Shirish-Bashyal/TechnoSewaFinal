using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Hubs.Model;

namespace Application.Hubs.InMemoryDB
{
    public class NotificationDb
    {
        private readonly ConcurrentDictionary<string, List<NotificationDto>> _notifications = new();

        public ConcurrentDictionary<string, List<NotificationDto>> notifications => _notifications;

        public void AddNotification(string userId, NotificationDto notificationDto)
        {
            _notifications.AddOrUpdate(
                userId,
                new List<NotificationDto> { notificationDto },
                (key, existingList) =>
                {
                    lock (existingList) // Lock to prevent concurrent modifications
                    {
                        existingList.Add(notificationDto);
                    }
                    return existingList;
                }
            );
        }

        public async Task<IEnumerable<NotificationDto>> GetByUserId(string userId)
        {
            var result = _notifications.TryGetValue(userId, out var notificationDto)
                ? notificationDto
                : new List<NotificationDto>();
            return result;
        }

        public bool RemoveNotification(string sellerId)
        {
            return _notifications.TryRemove(sellerId, out _);
        }
    }
}
