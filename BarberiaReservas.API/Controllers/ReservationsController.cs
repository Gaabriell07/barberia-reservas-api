using BarberiaReservas.Application.DTOs;
using BarberiaReservas.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BarberiaReservas.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _reservationService;
    private readonly INotificationService _notificationService;

    public ReservationsController(IReservationService reservationService, INotificationService notificationService)
    {
        _reservationService = reservationService;
        _notificationService = notificationService;
    }

    /// <summary>
    /// Obtiene todas las reservaciones
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAllReservations()
    {
        try
        {
            var reservations = await _reservationService.GetAllReservationsAsync();
            return Ok(reservations);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Obtiene una reservación por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetReservation(int id)
    {
        try
        {
            var reservation = await _reservationService.GetReservationAsync(id);
            return Ok(reservation);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Obtiene las reservaciones de un usuario
    /// </summary>
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserReservations(int userId)
    {
        try
        {
            var reservations = await _reservationService.GetUserReservationsAsync(userId);
            return Ok(reservations);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Crea una nueva reservación
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateReservation([FromBody] CreateReservationDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reservation = await _reservationService.CreateReservationAsync(dto);
            return Created(nameof(GetReservation), reservation);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Actualiza una reservación existente
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateReservation(int id, [FromBody] UpdateReservationDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reservation = await _reservationService.UpdateReservationAsync(id, dto);
            return Ok(reservation);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Cancela una reservación
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> CancelReservation(int id)
    {
        try
        {
            var result = await _reservationService.CancelReservationAsync(id);
            
            if (!result)
                return BadRequest(new { message = "No se pudo cancelar la reservación" });

            return Ok(new { message = "Reservación cancelada exitosamente" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}