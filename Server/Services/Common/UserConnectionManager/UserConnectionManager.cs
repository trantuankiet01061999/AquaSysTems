using System.Collections.Concurrent;

namespace AquaSolution.Server.Services.Common.UserConnectionManager;

public class UserConnectionManager : IUserConnectionManager
{
    private readonly ConcurrentDictionary<string, string> _connections = new();

    public void KeepConnection(string userId, string connectionId)
    {
        _connections[userId] = connectionId;
    }

    public void RemoveConnection(string connectionId)
    {
        var item = _connections.FirstOrDefault(x => x.Value == connectionId);
        if (!string.IsNullOrEmpty(item.Key))
            _connections.TryRemove(item.Key, out _);
    }

    public string? GetConnectionId(string userId)
    {
        _connections.TryGetValue(userId, out var connId);
        return connId;
    }
}
