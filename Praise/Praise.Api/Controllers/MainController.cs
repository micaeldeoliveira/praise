using Microsoft.AspNetCore.Mvc;
using Praise.Application.Interfaces.Notifications;

namespace Praise.Api.Controllers;

[ApiController]
public class MainController(INotification _notification) : ControllerBase
{
    
    protected IActionResult CustomResponse(object? result = null)
    {
        if (_notification.Has())
            foreach (var error in _notification.Notifications())
                ModelState.AddModelError(error.Key, error.Value);

        if (ModelState.IsValid)
            return Ok(result);

        var problemDetails = new ValidationProblemDetails(ModelState)
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Um ou mais erros de validação ocorreram",
            Instance = Request.Path
        };

        return BadRequest(problemDetails);
    }
}