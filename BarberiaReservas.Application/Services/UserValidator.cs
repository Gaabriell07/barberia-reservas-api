using System.Text.RegularExpressions;
using BarberiaReservas.Application.Interfaces;
using BarberiaReservas.Domain.Interfaces;

namespace BarberiaReservas.Application.Services;

public class UserValidator : IUserValidator
{
    private readonly IUserRepository _userRepository;

    public UserValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> IsEmailUniqueAsync(string email)
    {
        return !await _userRepository.ExistsEmailAsync(email);
    }

    public bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;
        var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, pattern);
    }

    public bool IsValidPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password)) return false;
        return password.Length >= 8;
    }

    public bool IsValidPhone(string? phone)
    {
        if (string.IsNullOrWhiteSpace(phone)) return true;
        return phone.Length >= 9 && phone.All(char.IsDigit);
    }
}