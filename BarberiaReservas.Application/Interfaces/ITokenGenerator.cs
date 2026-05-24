using BarberiaReservas.Domain.Entities;

namespace BarberiaReservas.Application.Interfaces;

public interface ITokenGenerator
{
    string GenerateToken(User user);
}
