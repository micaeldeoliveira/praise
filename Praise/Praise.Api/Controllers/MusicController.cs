using Microsoft.AspNetCore.Mvc;
using Praise.Application.Interfaces.Notifications;
using Praise.Application.Interfaces.Repositories;
using Praise.Application.Interfaces.Services;
using Praise.Core.Requests.Musics;
using Praise.Domain.Entities;

namespace Praise.Api.Controllers;

[Route("v1/musics")]
public class MusicController(
    INotification _notification,
    IMusicRepository _musicRepository,
    IMusicService _musicService)
    : MainController(_notification)
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _musicRepository.GetAllAsync();
        return Ok(result);
    }



    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Music))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] MusicRequest request)
    {
        var result = await _musicService.AddAsync(request);

        return CustomResponse(result);

    }
}
