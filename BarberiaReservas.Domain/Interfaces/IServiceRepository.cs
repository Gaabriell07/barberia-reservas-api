using BarberiaReservas.Domain.Entities;

namespace BarberiaReservas.Domain.Interfaces;

public interface IServiceRepository
{
    Task<IEnumerable<Service>> GetAllAsync();

    Task<Service?> GetByIdAsync(int id);

    Task<Service> AddAsync(Service service);

    Task UpdateAsync(Service service);

    Task DeleteAsync(Service service);
}