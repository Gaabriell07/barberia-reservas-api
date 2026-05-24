using System;
using System.Threading.Tasks;
using BarberiaReservas.Application.DTOs;
using BarberiaReservas.Application.Interfaces;
using BarberiaReservas.Domain.Entities;

namespace BarberiaReservas.Application.Services;

public class AuthService : IAuthService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IUserService _userService;

    public AuthService(
        IPasswordHasher passwordHasher,
        ITokenGenerator tokenGenerator,
        IUserService userService)
    {
        _passwordHasher = passwordHasher;
        _tokenGenerator = tokenGenerator;
        _userService = userService;
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await _userService.GetUserByEmailAsync(dto.Email);

        if (user == null)
            throw new Exception("Usuario no encontrado");

        var isPasswordValid = _passwordHasher.VerifyPassword(dto.Password, user.PasswordHash);

        if (!isPasswordValid)
            throw new Exception("Contraseña incorrecta");

        var token = _tokenGenerator.GenerateToken(user);

        return BuildAuthResponse(user, token);
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
    {
        var existingUser = await _userService.GetUserByEmailAsync(dto.Email);

        if (existingUser != null)
            throw new Exception("El email ya está registrado");

        var passwordHash = _passwordHasher.HashPassword(dto.Password);

        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            PasswordHash = passwordHash,
            Phone = dto.Phone,
            Role = "Client",
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        await _userService.CreateUserAsync(user);

        var token = _tokenGenerator.GenerateToken(user);

        return BuildAuthResponse(user, token);
    }

    private AuthResponseDto BuildAuthResponse(User user, string token)
    {
        return new AuthResponseDto
        {
            Token = token,
            Email = user.Email,
            Name = user.Name,
            Role = user.Role
        };
    }
}