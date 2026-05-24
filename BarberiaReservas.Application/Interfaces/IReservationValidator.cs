using BarberiaReservas.Application.DTOs;

namespace BarberiaReservas.Application.Interfaces;

public interface IReservationValidator
{
    Task<bool> ValidateAsync(CreateReservationDto dto);
    Task<bool> ValidateUpdateAsync(UpdateReservationDto dto);
    string? GetLastError();
}
