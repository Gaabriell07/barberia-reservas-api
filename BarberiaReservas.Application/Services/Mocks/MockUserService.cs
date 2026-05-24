using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarberiaReservas.Application.Interfaces;
using BarberiaReservas.Domain.Entities;

namespace BarberiaReservas.Application.Services.Mocks;

public class MockUserService : IUserService
{
    private readonly List<User> _users = new();

    public Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return Task.FromResult<IEnumerable<User>>(_users);
    }

    public Task<User?> GetUserByEmailAsync(string email)
    {
        var user = _users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(user);
    }

    public Task<User?> GetUserByIdAsync(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        return Task.FromResult(user);
    }

    public Task CreateUserAsync(User user)
    {
        user.Id = _users.Count > 0 ? _users.Max(u => u.Id) + 1 : 1;
        _users.Add(user);
        return Task.CompletedTask;
    }
}
