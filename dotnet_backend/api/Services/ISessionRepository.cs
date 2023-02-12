using api.Models;
using Newtonsoft.Json.Linq;

namespace api.Services;

public interface ISessionRepository
{
    Task<Session?> GetSessionOrNull(string sessionId);
    Task InsertSession(Session session);
    Task UpdateSessionWithTokens(string sessionId, string accessToken, string refreshToken);
}
