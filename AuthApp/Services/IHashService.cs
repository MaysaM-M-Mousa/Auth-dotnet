namespace AuthApp.Services;

public interface IHashService
{
    public string HashPassword(string password);
}
