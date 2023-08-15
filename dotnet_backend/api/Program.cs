using api.Gateways;
using api.Infrastructure;
using api.Middleware;
using api.Models.Settings;
using api.Services;

var builder = WebApplication.CreateBuilder(args);


var root = Directory.GetCurrentDirectory();
var dotenv = Path.Combine(root, ".env");
DotEnv.Load(dotenv);

builder.Services.AddControllers();

builder.Configuration.AddEnvironmentVariables();
builder.Services.Configure<MongoSettings>(
    builder.Configuration.GetSection("Mongo"));
builder.Services.Configure<SpotifyApiSettings>(
    builder.Configuration.GetSection("SpotifyApi"));
builder.Services.Configure<WebSettings>(
    builder.Configuration.GetSection("Web"));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => { options.Cookie.IsEssential = true; });

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddScoped<ISessionRepository, MongoSessionRepository>();
builder.Services.AddScoped<UserContext>();
builder.Services.AddScoped<SpotifyApiGateway>();
builder.Services.AddScoped<SpotifyAuthorizationService>();
builder.Services.AddScoped<ExportService>();
builder.Services.AddScoped<CsvGenerator>();

var app = builder.Build();

app.UseRouting();
app.UseSession();
app.UseSpotifyAuthentication();
app.MapControllers();

app.Run();