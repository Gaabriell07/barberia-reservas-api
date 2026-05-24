using BarberiaReservas.Application.DTOs;
using BarberiaReservas.Application.Interfaces;
using BarberiaReservas.Domain.Entities;
using BarberiaReservas.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BarberiaReservas.Application.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationValidator _validator;
    private readonly IReservationStateManager _stateManager;
    private readonly AppDbContext _context;

    public ReservationService(
        IReservationValidator validator,
        IReservationStateManager stateManager,
        AppDbContext context)
    {
        _validator = validator;
        _stateManager = stateManager;
        _context = context;
    }

    public async Task<ReservationResponseDto> GetReservationAsync(int id)
    {
        try
        {
            if (id <= 0)
                throw new Exception("ID inválido");

            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null)
                throw new Exception("Reservación no encontrada");

            return MapToDto(reservation);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al obtener la reservación: {ex.Message}");
        }
    }

    public async Task<IEnumerable<ReservationResponseDto>> GetUserReservationsAsync(int userId)
    {
        try
        {
            if (userId <= 0)
                throw new Exception("UserId inválido");

            var reservations = await _context.Reservations
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.DateTime)
                .ToListAsync();

            return reservations.Select(MapToDto);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al obtener las reservaciones del usuario: {ex.Message}");
        }
    }

    public async Task<IEnumerable<ReservationResponseDto>> GetAllReservationsAsync()
    {
        try
        {
            var reservations = await _context.Reservations
                .OrderByDescending(r => r.DateTime)
                .ToListAsync();

            return reservations.Select(MapToDto);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al obtener todas las reservaciones: {ex.Message}");
        }
    }

    public async Task<ReservationResponseDto> CreateReservationAsync(CreateReservationDto dto)
    {
        try
        {
            // Validar el DTO
            var isValid = await _validator.ValidateAsync(dto);
            if (!isValid)
                throw new Exception(_validator.GetLastError() ?? "Validación fallida");

            // Crear la nueva reservación
            var reservation = new Reservation
            {
                UserId = dto.UserId,
                ServiceId = dto.ServiceId,
                DateTime = dto.DateTime,
                Notes = dto.Notes,
                Status = "Pending",
                CreatedAt = DateTime.UtcNow
            };

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return MapToDto(reservation);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al crear la reservación: {ex.Message}");
        }
    }

    public async Task<ReservationResponseDto> UpdateReservationAsync(int id, UpdateReservationDto dto)
    {
        try
        {
            if (id <= 0)
                throw new Exception("ID inválido");

            // Validar el DTO
            var isValid = await _validator.ValidateUpdateAsync(dto);
            if (!isValid)
                throw new Exception(_validator.GetLastError() ?? "Validación fallida");

            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null)
                throw new Exception("Reservación no encontrada");

            // Actualizar los campos
            reservation.ServiceId = dto.ServiceId;
            reservation.DateTime = dto.DateTime;
            reservation.Notes = dto.Notes;
            reservation.Status = dto.Status;
            reservation.UpdatedAt = DateTime.UtcNow;

            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();

            return MapToDto(reservation);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al actualizar la reservación: {ex.Message}");
        }
    }

    public async Task<bool> CancelReservationAsync(int reservationId)
    {
        try
        {
            if (reservationId <= 0)
                return false;

            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(r => r.Id == reservationId);

            if (reservation == null)
                return false;

            reservation.Status = "Cancelled";
            reservation.UpdatedAt = DateTime.UtcNow;

            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();

            await _stateManager.CancelReservationAsync(reservationId);

            return true;
        }
        catch
        {
            return false;
        }
    }

    private ReservationResponseDto MapToDto(Reservation reservation)
    {
        return new ReservationResponseDto
        {
            Id = reservation.Id,
            UserId = reservation.UserId,
            ServiceId = reservation.ServiceId,
            DateTime = reservation.DateTime,
            Status = reservation.Status,
            Notes = reservation.Notes,
            CreatedAt = reservation.CreatedAt,
            UpdatedAt = reservation.UpdatedAt
        };
    }
}
