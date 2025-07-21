using System.Collections.Concurrent;

namespace Application.Hubs.InMemoryDB
{
    public class UserConnectionDb
    {
        private readonly ConcurrentDictionary<string, string> _connections = new();

        public ConcurrentDictionary<string, string> connections => _connections;

        public void AddConnection(string userId, string connectionId)
        {
            _connections[userId] = connectionId;
        }

        public bool RemoveConnection(string connectionId)
        {
            return _connections.TryRemove(connectionId, out _);
        }

        public string? GetConnection(string userId)
        {
            _connections.TryGetValue(userId, out var connectionId);
            return connectionId;
        }
    }
}
