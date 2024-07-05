using JwtAuth.Options;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;
using System.Text;

namespace JwtAuth.Services;

public class HashService : IHashService
{
    private readonly HashOptions _hashOptions;

    public HashService(IOptions<HashOptions> hashOptions)
    {
        _hashOptions = hashOptions.Value;
    }

    public string HashPassword(string password)
    {
        string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password!,
            salt: Encoding.ASCII.GetBytes(_hashOptions.SaltValue),
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: _hashOptions.IterationCount,
            numBytesRequested: _hashOptions.NumBytesRequested));

        return hashedPassword;
    }
}
