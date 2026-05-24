using System.Collections.Generic;
using System.Threading.Tasks;
using BarberiaReservas.Domain.Entities;

namespace BarberiaReservas.Application.Interfaces;

public interface IUserService
{
    Task<User?> GetUserByIdAsync(int id);
    Task<IEnumerable<User>> GetAllUsersAsync();
    // Métodos añadidos para que el módulo de Auth pueda delegar la persistencia respetando SRP
    Task<User?> GetUserByEmailAsync(string email);
    Task CreateUserAsync(User user);
}
