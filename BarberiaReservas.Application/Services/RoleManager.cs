using BarberiaReservas.Application.Interfaces;
using BarberiaReservas.Domain.Interfaces;

namespace BarberiaReservas.Application.Services;

public class RoleManager : IRoleManager
{
    private readonly IUserRepository _userRepository;
    private readonly string[] _validRoles = { "Client", "Admin" };

    public RoleManager(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public bool IsValidRole(string role)
    {
        return _validRoles.Contains(role);
    }

    public async Task<bool> PromoteToAdminAsync(int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null) return false;

        user.Role = "Admin";
        await _userRepository.UpdateAsync(user);
        return true;
    }

    public async Task<bool> DemoteToClientAsync(int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null) return false;

        user.Role = "Client";
        await _userRepository.UpdateAsync(user);
        return true;
    }
}