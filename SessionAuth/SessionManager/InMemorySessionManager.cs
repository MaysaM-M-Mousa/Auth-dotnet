namespace SessionAuth.SessionManager;

public class InMemorySessionManager : ISessionManager
{
    private readonly Dictionary<Guid, List<Session>> _sessions;

    public InMemorySessionManager()
    {
        _sessions = new();
    }

    public Task<Session> CreateSession(Guid userId, DateTime expirationTime)
    {
        var session = new Session(userId, Guid.NewGuid().ToString("N"), expirationTime);

        if (!_sessions.ContainsKey(userId))
        {
            _sessions.Add(userId, new() { session });
        }
        else
        {
            _sessions[userId].Add(session);
        }

        return Task.FromResult(session);
    }

    public Task<Guid?> GetUserBySessionValue(string sessionValue)
    {
        var userId = _sessions
            .Where(kvp => kvp.Value.Any(session => session.Value == sessionValue))
            .Select(kvp => (Guid?)kvp.Key)
            .FirstOrDefault();

        return Task.FromResult(userId);
    }

    public Task<bool> RevokeSession(string sessionValue)
    {
        var session = _sessions
            .SelectMany(s => s.Value)
            .FirstOrDefault(s => s.Value == sessionValue);

        if (session is null)
        {
            return Task.FromResult(false);
        }

        session.Revoke();

        return Task.FromResult(true);
    }
}
