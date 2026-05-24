using BarberiaReservas.Application.DTOs;
using BarberiaReservas.Application.Interfaces;

namespace BarberiaReservas.Application.Services;

public class ReservationValidator : IReservationValidator
{
    private string? _lastError;

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

        return await Task.FromResult(true);
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
