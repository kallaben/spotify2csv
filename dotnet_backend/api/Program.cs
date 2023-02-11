using api.Infrastructure;
using api.Middleware;
using api.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.Cookie.IsEssential = true;
});

builder.Services.AddScoped<ISessionRepository, MongoSessionRepository>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseSession();
app.UseSpotifyAuthentication();
app.MapControllers();

app.Run();
