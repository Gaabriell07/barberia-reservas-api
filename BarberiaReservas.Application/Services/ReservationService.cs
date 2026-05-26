using BarberiaReservas.Application.DTOs;
using BarberiaReservas.Application.Interfaces;
using BarberiaReservas.Domain.Entities;
using BarberiaReservas.Domain.Interfaces; // <-- IMPORTANTE: Cambiamos Infrastructure por Domain

namespace BarberiaReservas.Application.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationValidator _validator;
    private readonly IReservationStateManager _stateManager;
    private readonly IReservationRepository _repository; // <-- Inyectamos el Repositorio en vez del DbContext

    public ReservationService(
        IReservationValidator validator,
        IReservationStateManager stateManager,
        IReservationRepository repository)
    {
        _validator = validator;
        _stateManager = stateManager;
        _repository = repository;
    }

    public async Task<ReservationResponseDto> GetReservationAsync(int id)
    {
        try
        {
            if (id <= 0)
                throw new Exception("ID inválido");

            var reservation = await _repository.GetByIdAsync(id);

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

            var reservations = await _repository.GetByUserIdAsync(userId);

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
            var reservations = await _repository.GetAllAsync();

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

            await _repository.AddAsync(reservation);

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

            var reservation = await _repository.GetByIdAsync(id);

            if (reservation == null)
                throw new Exception("Reservación no encontrada");

            // Actualizar los campos
            reservation.ServiceId = dto.ServiceId;
            reservation.DateTime = dto.DateTime;
            reservation.Notes = dto.Notes;
            reservation.Status = dto.Status;
            reservation.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(reservation);

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

            var reservation = await _repository.GetByIdAsync(reservationId);

            if (reservation == null)
                return false;

            reservation.Status = "Cancelled";
            reservation.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(reservation);

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
