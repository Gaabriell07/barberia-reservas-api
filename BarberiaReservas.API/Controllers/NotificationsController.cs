using BarberiaReservas.Application.DTOs;
using BarberiaReservas.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BarberiaReservas.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly INotificationService _notificationService;
    private readonly INotificationLogRepository _logRepository;

    public NotificationsController(
        INotificationService notificationService,
        INotificationLogRepository logRepository)
    {
        _notificationService = notificationService;
        _logRepository = logRepository;
    }

    [HttpPost("send")]
    public async Task<IActionResult> Send([FromBody] NotificationDto dto)
    {
        var result = await _notificationService.SendAsync(dto);

        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("logs")]
    public async Task<IActionResult> GetLogs()
    {
        var logs = await _logRepository.GetAllAsync();
        return Ok(logs);
    }
}