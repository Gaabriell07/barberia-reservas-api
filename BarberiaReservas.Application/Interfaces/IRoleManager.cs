namespace BarberiaReservas.Application.Interfaces;

public interface IRoleManager
{
    bool IsValidRole(string role);
    Task<bool> PromoteToAdminAsync(int userId);
    Task<bool> DemoteToClientAsync(int userId);
}