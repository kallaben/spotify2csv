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
    public async Task<string> Export(IEnumerable<string> playlistIds)
    {
        return await _exportService.GetSpotifyPlaylistsAsCsv(playlistIds);
    }
    
    [Route("playlists")]
    [HttpGet]
    public async Task<IEnumerable<PlaylistDto>> GetPlaylists()
    {
        return await _exportService.GetPlaylists();
    }
}
