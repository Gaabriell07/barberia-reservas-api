using BarberiaReservas.Application.DTOs;

namespace BarberiaReservas.Application.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserResponseDto>> GetAllAsync();
    Task<UserResponseDto?> GetByIdAsync(int id);
    Task<UserResponseDto> CreateAsync(CreateUserDto dto);
    Task<UserResponseDto> UpdateAsync(int id, UpdateUserDto dto);
    Task<bool> DeactivateAsync(int id);
    Task<bool> ChangePasswordAsync(ChangePasswordDto dto);
    Task<PagedResultDto<UserResponseDto>> GetPagedAsync(UserQueryDto query);
    Task<IEnumerable<UserResponseDto>> GetByRoleAsync(string roleName);
}
