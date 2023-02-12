using api.Gateways;
using api.Infrastructure;
using api.Infrastructure.Models;
using api.Middleware;
using api.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.Configure<MongoSettings>(
    builder.Configuration.GetSection("Mongo"));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddScoped<ISessionRepository, MongoSessionRepository>();
builder.Services.AddScoped<UserContext>();
builder.Services.AddScoped<SpotifyApiGateway>();
builder.Services.AddScoped<SpotifyAuthorizationService>();
builder.Services.AddScoped<ExportService>();

var app = builder.Build();

app.UseRouting();
app.UseSession();
app.UseSpotifyAuthentication();
app.MapControllers();

app.Run();
