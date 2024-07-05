namespace JwtAuth.Options;

public class HashOptions
{
    public string SaltValue { get; set; }

    public int IterationCount { get; set; }

    public int NumBytesRequested { get; set; }
}
