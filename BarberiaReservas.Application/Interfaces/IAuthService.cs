using System.Threading.Tasks;
using BarberiaReservas.Application.DTOs;

namespace BarberiaReservas.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> LoginAsync(LoginDto dto);
    Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
}
