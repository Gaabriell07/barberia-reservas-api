using BarberiaReservas.Domain.Interfaces;

namespace BarberiaReservas.Application.Interfaces;

public interface IUserValidator
{
    Task<bool> IsEmailUniqueAsync(string email);
    bool IsValidEmail(string email);
    bool IsValidPassword(string password);
    bool IsValidPhone(string? phone);
}