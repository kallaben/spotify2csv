using api.Infrastructure.Models;
using api.Models;
using api.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace api.Infrastructure;

public class MongoSessionRepository: ISessionRepository
{
    private readonly IMongoCollection<Session> _sessionsCollection;

    public MongoSessionRepository(IOptions<MongoSettings> mongoSettings)
    {
        var mongoClient = new MongoClient(
            mongoSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            mongoSettings.Value.DatabaseName);

        _sessionsCollection = mongoDatabase.GetCollection<Session>(
            mongoSettings.Value.SessionCollectionName);
    }
    public async Task<Session?> GetSessionOrNull(string sessionId)
    {
        return await _sessionsCollection
            .Find(session => session.SessionId == sessionId)
            .FirstOrDefaultAsync();
    }
    
    public async Task InsertSession(Session session)
    {
        await _sessionsCollection.InsertOneAsync(session);
    }

    public async Task UpdateSessionWithTokens(string sessionId, SpotifyAuthentication spotifyAuthentication)
    {
        var session = await GetSessionOrNull(sessionId);
        session.SpotifyAuthentication = spotifyAuthentication;
        await _sessionsCollection.ReplaceOneAsync(x => x.Id == session.Id, session);
    }
}
