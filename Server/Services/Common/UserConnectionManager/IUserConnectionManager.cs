namespace AquaSolution.Server.Services.Common.UserConnectionManager
{
    public interface IUserConnectionManager
    {
        void KeepConnection(string userId, string connectionId);
        void RemoveConnection(string connectionId);
        string? GetConnectionId(string userId);
    }
}
