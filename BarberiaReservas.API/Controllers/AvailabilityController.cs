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

    /// <summary>
    /// Obtiene los slots disponibles para una fecha específica y servicio.
    /// </summary>
    /// <param name="date">Fecha en formato YYYY-MM-DD</param>
    /// <param name="serviceId">ID del servicio (ahora asume 30 min por defecto)</param>
    [HttpGet("slots")]
    public async Task<IActionResult> GetAvailableSlots([FromQuery] string date, [FromQuery] int serviceId = 1)
    {
        try
        {
            if (!DateTime.TryParse(date, out var parsedDate))
            {
                return BadRequest(new { message = "Fecha inválida. Usa formato YYYY-MM-DD." });
            }

            var slots = await _availabilityService.GetAvailableSlotsAsync(parsedDate, serviceId);
            return Ok(new
            {
                date = parsedDate.Date.ToString("yyyy-MM-dd"),
                serviceId = serviceId,
                availableSlots = slots
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al obtener slots disponibles.", error = ex.Message });
        }
    }

    /// <summary>
    /// Verifica si un slot específico está disponible.
    /// </summary>
    /// <param name="dateTime">DateTime en formato ISO 8601 (ej: 2024-12-20T10:30:00)</param>
    /// <param name="durationMinutes">Duración del servicio en minutos</param>
    [HttpGet("check")]
    public async Task<IActionResult> CheckAvailability([FromQuery] string dateTime, [FromQuery] int durationMinutes = 30)
    {
        try
        {
            if (!DateTime.TryParse(dateTime, out var parsedDateTime))
            {
                return BadRequest(new { message = "DateTime inválido. Usa formato ISO 8601." });
            }

            var isAvailable = await _availabilityService.IsTimeSlotAvailableAsync(parsedDateTime, durationMinutes);
            return Ok(new
            {
                dateTime = parsedDateTime,
                durationMinutes = durationMinutes,
                isAvailable = isAvailable
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al verificar disponibilidad.", error = ex.Message });
        }
    }
}
