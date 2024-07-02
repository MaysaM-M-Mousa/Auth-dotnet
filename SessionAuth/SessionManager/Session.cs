namespace SessionAuth.SessionManager;

public class Session
{
    public Guid UserId { get; private set; }

    public string Value { get; private set; }

    public bool IsRevoked { get; private set; } = false;

    public DateTime ExpirationDate { get; private set; }

    public Session(
        Guid userId, 
        string value, 
        DateTime expirationDate)
    {
        UserId = userId;
        Value = value;
        IsRevoked = false;
        ExpirationDate = expirationDate;
    }

    public bool IsExpired() => DateTime.UtcNow > ExpirationDate;

    public void Revoke() => IsRevoked = true;
}
