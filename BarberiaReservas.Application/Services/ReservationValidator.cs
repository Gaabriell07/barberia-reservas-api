using BarberiaReservas.Application.DTOs;
using BarberiaReservas.Application.Interfaces;

namespace BarberiaReservas.Application.Services;

public class ReservationValidator : IReservationValidator
{
    // Duración temporal hasta que exista un repositorio de servicios para leer Service.DurationMinutes.
    private const int DefaultDurationMinutes = 30;

    private readonly IAvailabilityService _availabilityService;
    private string? _lastError;

    public ReservationValidator(IAvailabilityService availabilityService)
    {
        _availabilityService = availabilityService;
    }

    public async Task<bool> ValidateAsync(CreateReservationDto dto)
    {
        if (dto == null)
        {
            _lastError = "El DTO no puede ser nulo";
            return false;
        }

        if (dto.UserId <= 0)
        {
            _lastError = "El UserId debe ser mayor a 0";
            return false;
        }

        if (dto.ServiceId <= 0)
        {
            _lastError = "El ServiceId debe ser mayor a 0";
            return false;
        }

        if (dto.DateTime <= DateTime.Now)
        {
            _lastError = "La fecha y hora de la reservación debe ser en el futuro";
            return false;
        }

        var isAvailable = await _availabilityService.IsTimeSlotAvailableAsync(dto.DateTime, DefaultDurationMinutes, dto.BarberId);
        if (!isAvailable)
        {
            _lastError = "El horario solicitado no está disponible";
            return false;
        }

        return true;
    }

    public async Task<bool> ValidateUpdateAsync(UpdateReservationDto dto)
    {
        if (dto == null)
        {
            _lastError = "El DTO no puede ser nulo";
            return false;
        }

        if (dto.Id <= 0)
        {
            _lastError = "El ID debe ser mayor a 0";
            return false;
        }

        if (dto.UserId <= 0)
        {
            _lastError = "El UserId debe ser mayor a 0";
            return false;
        }

        if (dto.ServiceId <= 0)
        {
            _lastError = "El ServiceId debe ser mayor a 0";
            return false;
        }

        if (dto.DateTime <= DateTime.Now)
        {
            _lastError = "La fecha y hora de la reservación debe ser en el futuro";
            return false;
        }

        return await Task.FromResult(true);
    }

    public string? GetLastError()
    {
        return _lastError;
    }
}
