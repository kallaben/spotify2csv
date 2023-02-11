namespace api.Models;

public class Session
{
    public string SessionId { get; set; }
    public string? AuthenticationToken { get; set; }

    public bool HasValidAuthenticationToken()
    {
        return AuthenticationToken != null;
    }
}
