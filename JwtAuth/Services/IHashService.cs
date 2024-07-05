namespace JwtAuth.Services;

public interface IHashService
{
    public string HashPassword(string password);
}
