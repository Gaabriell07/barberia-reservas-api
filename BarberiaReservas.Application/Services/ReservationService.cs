using BarberiaReservas.Application.DTOs;
using BarberiaReservas.Application.Interfaces;
using BarberiaReservas.Domain.Entities;
using BarberiaReservas.Domain.Interfaces;

namespace BarberiaReservas.Application.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationValidator _validator;
    private readonly IReservationStateManager _stateManager;
    private readonly IReservationRepository _repository;
    private readonly INotificationService _notificationService;

    public ReservationService(
        IReservationValidator validator,
        IReservationStateManager stateManager,
        IReservationRepository repository,
        INotificationService notificationService)
    {
        _validator = validator;
        _stateManager = stateManager;
        _repository = repository;
        _notificationService = notificationService;
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
            
            var isValid = await _validator.ValidateAsync(dto);
            if (!isValid)
                throw new Exception(_validator.GetLastError() ?? "Validación fallida");

            var reservation = new Reservation
            {
                UserId = dto.UserId,
                ServiceId = dto.ServiceId,
                BarberId = dto.BarberId,
                DateTime = dto.DateTime,
                Notes = dto.Notes,
                Status = "Pending",
                CreatedAt = DateTime.UtcNow
            };

            var createdReservation = await _repository.CreateAsync(reservation);

            // Recargar con navegaciones (User/Service) para poder notificar y mapear nombres correctamente.
            var fullReservation = await _repository.GetByIdAsync(createdReservation.Id) ?? createdReservation;

            await NotifyReservationAsync(fullReservation, "ReservationCreated", "Confirmación de reserva");

            return MapToDto(fullReservation);
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

            var isValid = await _validator.ValidateUpdateAsync(dto);
            if (!isValid)
                throw new Exception(_validator.GetLastError() ?? "Validación fallida");

            var reservation = await _repository.GetByIdAsync(id);

            if (reservation == null)
                throw new Exception("Reservación no encontrada");

            reservation.ServiceId = dto.ServiceId;
            reservation.DateTime = dto.DateTime;
            reservation.Notes = dto.Notes;
            reservation.Status = dto.Status;
            reservation.UpdatedAt = DateTime.UtcNow;

            var updatedReservation = await _repository.UpdateAsync(reservation);

            return MapToDto(updatedReservation);
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

            await NotifyReservationAsync(reservation, "ReservationCancelled", "Cancelación de reserva");

            return true;
        }
        catch
        {
            return false;
        }
    }

    private async Task NotifyReservationAsync(Reservation reservation, string templateKey, string subject)
    {
        try
        {
            await _notificationService.SendAsync(new NotificationDto
            {
                Channel = "Email",
                Recipient = reservation.User?.Email ?? string.Empty,
                Subject = subject,
                TemplateKey = templateKey,
                Variables = new Dictionary<string, string>
                {
                    ["ClientName"] = reservation.User?.Name ?? $"Cliente #{reservation.UserId}",
                    ["ServiceName"] = reservation.Service?.Name ?? $"Servicio #{reservation.ServiceId}",
                    ["DateTime"] = reservation.DateTime.ToString("g")
                }
            });
        }
        catch
        {
            // La notificación es de mejor esfuerzo: no debe hacer fallar la operación de reserva.
        }
    }

    public async Task<ReservationReportDto> GetReservationReportAsync()
    {
        try
        {
            var reservations = await _repository.GetAllAsync();

            var activeReservations = reservations.Where(r => r.Status != "Cancelled");

            return new ReservationReportDto
            {
                TotalReservations = reservations.Count(),
                CompletedReservations = reservations.Count(r => r.Status == "Completed"),
                CancelledReservations = reservations.Count(r => r.Status == "Cancelled"),
                EstimatedRevenue = activeReservations.Sum(r => r.Service?.Price ?? 0)
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al generar el reporte de reservaciones: {ex.Message}");
        }
    }

    private ReservationResponseDto MapToDto(Reservation reservation)
    {
        return new ReservationResponseDto
        {
            Id          = reservation.Id,
            UserId      = reservation.UserId,
            UserName    = reservation.User?.Name    ?? $"Cliente #{reservation.UserId}",
            ServiceId   = reservation.ServiceId,
            ServiceName = reservation.Service?.Name ?? $"Servicio #{reservation.ServiceId}",
            BarberId    = reservation.BarberId,
            BarberName  = reservation.Barber?.Name ?? $"Barbero #{reservation.BarberId}",
            DateTime    = reservation.DateTime,
            Status      = reservation.Status,
            Notes       = reservation.Notes,
            CreatedAt   = reservation.CreatedAt,
            UpdatedAt   = reservation.UpdatedAt
        };
    }
}