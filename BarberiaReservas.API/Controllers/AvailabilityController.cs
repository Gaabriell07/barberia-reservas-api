using BarberiaReservas.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BarberiaReservas.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AvailabilityController : ControllerBase
{
    private readonly IAvailabilityService _availabilityService;

    public AvailabilityController(IAvailabilityService availabilityService)
    {
        _availabilityService = availabilityService;
    }

    [HttpGet("slots")]
    public async Task<IActionResult> GetAvailableSlots([FromQuery] string date, [FromQuery] int serviceId = 1, [FromQuery] int barberId = 1)
    {
        try
        {
            if (!DateTime.TryParse(date, out var parsedDate))
            {
                return BadRequest(new { message = "Fecha inválida. Usa formato YYYY-MM-DD." });
            }

            var slots = await _availabilityService.GetAvailableSlotsAsync(parsedDate, serviceId, barberId);
            return Ok(new
            {
                date = parsedDate.Date.ToString("yyyy-MM-dd"),
                serviceId = serviceId,
                barberId = barberId,
                availableSlots = slots
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al obtener slots disponibles.", error = ex.Message });
        }
    }

    [HttpGet("check")]
    public async Task<IActionResult> CheckAvailability([FromQuery] string dateTime, [FromQuery] int durationMinutes = 30, [FromQuery] int barberId = 1)
    {
        try
        {
            if (!DateTime.TryParse(dateTime, out var parsedDateTime))
            {
                return BadRequest(new { message = "DateTime inválido. Usa formato ISO 8601." });
            }

            var isAvailable = await _availabilityService.IsTimeSlotAvailableAsync(parsedDateTime, durationMinutes, barberId);
            return Ok(new
            {
                dateTime = parsedDateTime,
                durationMinutes = durationMinutes,
                barberId = barberId,
                isAvailable = isAvailable
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al verificar disponibilidad.", error = ex.Message });
        }
    }
}
