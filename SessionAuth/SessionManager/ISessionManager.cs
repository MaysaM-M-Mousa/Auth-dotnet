namespace SessionAuth.SessionManager;

public interface ISessionManager
{
    Task<Session> CreateSession(Guid userId, DateTime expirationTime);

    Task<Guid?> GetUserBySessionValue(string sessionValue);

    Task<bool> RevokeSession(string sessionValue);
}
