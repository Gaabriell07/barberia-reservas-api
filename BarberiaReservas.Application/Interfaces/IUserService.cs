using System.Collections.Generic;
using System.Threading.Tasks;
using BarberiaReservas.Domain.Entities;

namespace BarberiaReservas.Application.Interfaces;

public interface IUserService
{
    Task<User?> GetUserByIdAsync(int id);
    Task<IEnumerable<User>> GetAllUsersAsync();
}
