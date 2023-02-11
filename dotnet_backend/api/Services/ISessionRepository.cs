using api.Models;

namespace api.Services;

public interface ISessionRepository
{
    Session? getSessionOrNull(string sessionId);
}
