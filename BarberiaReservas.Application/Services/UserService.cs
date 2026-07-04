using BarberiaReservas.Application.DTOs;
using BarberiaReservas.Application.Interfaces;
using BarberiaReservas.Domain.Entities;
using BarberiaReservas.Domain.Interfaces;

namespace BarberiaReservas.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserValidator _userValidator;
    private readonly IRoleManager _roleManager;

    public UserService(
        IUserRepository userRepository,
        IUserValidator userValidator,
        IRoleManager roleManager)
    {
        _userRepository = userRepository;
        _userValidator = userValidator;
        _roleManager = roleManager;
    }

    public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(MapToResponseDto);
    }

    public async Task<UserResponseDto?> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return null;
        return MapToResponseDto(user);
    }

    public async Task<UserResponseDto> CreateAsync(CreateUserDto dto)
    {
        if (!_userValidator.IsValidEmail(dto.Email))
            throw new ArgumentException("El email no tiene un formato válido.");

        if (!await _userValidator.IsEmailUniqueAsync(dto.Email))
            throw new ArgumentException("El email ya está registrado.");

        if (!_userValidator.IsValidPassword(dto.Password))
            throw new ArgumentException("La contraseña debe tener al menos 8 caracteres.");

        if (!_userValidator.IsValidPhone(dto.Phone))
            throw new ArgumentException("El teléfono no es válido.");

        if (!_roleManager.IsValidRole(dto.Role))
            throw new ArgumentException("El rol no es válido.");

        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Phone = dto.Phone,
            Role = dto.Role,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        var created = await _userRepository.CreateAsync(user);
        return MapToResponseDto(created);
    }

    public async Task<UserResponseDto> UpdateAsync(int id, UpdateUserDto dto)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            throw new KeyNotFoundException("Usuario no encontrado.");

        if (!_userValidator.IsValidPhone(dto.Phone))
            throw new ArgumentException("El teléfono no es válido.");

        user.Name = dto.Name;
        user.Phone = dto.Phone;

        var updated = await _userRepository.UpdateAsync(user);
        return MapToResponseDto(updated);
    }

    public async Task<bool> DeactivateAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return false;

        if (!user.IsActive)
            throw new InvalidOperationException("El usuario ya está desactivado.");

        user.IsActive = false;
        await _userRepository.UpdateAsync(user);
        return true;
    }

    public async Task<PagedResultDto<UserResponseDto>> GetPagedAsync(UserQueryDto query)
    {
        var (users, totalCount) = await _userRepository.GetPagedAsync(
            query.PageNumber,
            query.PageSize,
            query.SearchTerm);

        return new PagedResultDto<UserResponseDto>
        {
            Items = users.Select(MapToResponseDto),
            TotalCount = totalCount,
            PageNumber = query.PageNumber,
            PageSize = query.PageSize
        };
    }

    private UserResponseDto MapToResponseDto(User user)
    {
        return new UserResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role,
            Phone = user.Phone,
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt
        };
    }

    public async Task<bool> ChangePasswordAsync(ChangePasswordDto dto)
    {
        if (dto.NewPassword != dto.ConfirmPassword)
            throw new ArgumentException("La nueva contraseña y la confirmación no coinciden.");

        if (!_userValidator.IsValidPassword(dto.NewPassword))
            throw new ArgumentException("La nueva contraseña debe tener al menos 8 caracteres.");

        var user = await _userRepository.GetByIdAsync(dto.UserId);
        if (user == null)
            throw new KeyNotFoundException("Usuario no encontrado.");

        if (!BCrypt.Net.BCrypt.Verify(dto.CurrentPassword, user.PasswordHash))
            throw new ArgumentException("La contraseña actual es incorrecta.");

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
        await _userRepository.UpdateAsync(user);
        return true;
    }
    public async Task<IEnumerable<UserResponseDto>> GetByRoleAsync(string roleName)
    {
        if (!_roleManager.IsValidRole(roleName))
            throw new ArgumentException("El rol especificado no es válido.");

        var users = await _userRepository.GetByRoleAsync(roleName);
        return users.Select(MapToResponseDto);
    }
}