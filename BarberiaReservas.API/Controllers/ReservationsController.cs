using BarberiaReservas.Application.DTOs;
using BarberiaReservas.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BarberiaReservas.API.Controllers;

[Authorize]
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

    [Authorize(Roles = "Admin,Barber")]
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

    [HttpGet("reports")]
    public async Task<IActionResult> GetReservationReport()
    {
        try
        {
            var report = await _reservationService.GetReservationReportAsync();
            return Ok(report);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetReservation(int id)
    {
        try
        {
            var reservation = await _reservationService.GetReservationAsync(id);
            if (reservation == null) return NotFound("Reservación no encontrada.");

            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (currentUserRole != "Admin" && currentUserRole != "Barber" && reservation.UserId.ToString() != currentUserId)
            {
                return Forbid();
            }

            return Ok(reservation);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserReservations(int userId)
    {
        try
        {
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (currentUserRole != "Admin" && currentUserRole != "Barber" && userId.ToString() != currentUserId)
            {
                return Forbid();
            }

            var reservations = await _reservationService.GetUserReservationsAsync(userId);
            return Ok(reservations);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateReservation([FromBody] CreateReservationDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (currentUserRole != "Admin" && dto.UserId.ToString() != currentUserId)
            {
                return Forbid();
            }

            var reservation = await _reservationService.CreateReservationAsync(dto);
            return Created(nameof(GetReservation), reservation);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateReservation(int id, [FromBody] UpdateReservationDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reservation = await _reservationService.GetReservationAsync(id);
            if (reservation == null) return NotFound("Reservación no encontrada.");

            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (currentUserRole != "Admin" && reservation.UserId.ToString() != currentUserId)
            {
                return Forbid();
            }

            var updated = await _reservationService.UpdateReservationAsync(id, dto);
            return Ok(updated);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> CancelReservation(int id)
    {
        try
        {
            var reservation = await _reservationService.GetReservationAsync(id);
            if (reservation == null) return NotFound("Reservación no encontrada.");

            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (currentUserRole != "Admin" && reservation.UserId.ToString() != currentUserId)
            {
                return Forbid();
            }

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