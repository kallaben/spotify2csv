using api.Models;

namespace api.Services;

public interface ISessionRepository
{
    Task<Session?> GetSessionOrNull(string sessionId);
    Task InsertSession(Session session);
}
