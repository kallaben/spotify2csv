using api.Models;
using api.Services;

namespace api.Infrastructure;

public class MongoSessionRepository: ISessionRepository
{
    public Session? getSessionOrNull(string sessionId)
    {
        return null;
    }
}
