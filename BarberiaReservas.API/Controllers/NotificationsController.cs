using BarberiaReservas.Application.DTOs;
using BarberiaReservas.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BarberiaReservas.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public NotificationsController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpPost("send")]
    public async Task<IActionResult> Send([FromBody] NotificationDto dto)
    {
        var result = await _notificationService.SendAsync(dto);

        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }
}