using api.Infrastructure;
using api.Middleware;
using api.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ISessionRepository, MongoSessionRepository>();


var app = builder.Build();

app.UseRouting();
app.UseSession();
app.UseSpotifyAuthentication();
app.MapControllers();

app.Run();
