using System.Text;
using api.Models.Dtos;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class CsvExportController : ControllerBase
{
    private readonly ExportService _exportService;

    public CsvExportController(ExportService exportService)
    {
        _exportService = exportService;
    }

    [Route("export")]
    [HttpGet]
    public async Task<FileResult> Export([FromQuery] string[] playlistIds)
    {
        var csv = await _exportService.GetSpotifyPlaylistsAsCsv(playlistIds);
        var filename = $"{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}.csv";

        return File(Encoding.UTF8.GetBytes(csv), "text/csv", filename);
    }

    [Route("playlists")]
    [HttpGet]
    public async Task<IEnumerable<PlaylistDto>> GetPlaylists()
    {
        return await _exportService.GetPlaylists();
    }
}