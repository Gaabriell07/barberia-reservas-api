using BarberiaReservas.Domain.Entities;

namespace BarberiaReservas.Application.Interfaces;

public interface IServiceManager
{
    Task<IEnumerable<Service>> GetAllServicesAsync();

    Task<Service?> GetServiceByIdAsync(int id);

    Task<Service> CreateServiceAsync(Service service);

    Task UpdateServiceAsync(Service service);

    Task DeleteServiceAsync(int id);
}