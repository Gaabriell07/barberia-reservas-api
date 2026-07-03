using BarberiaReservas.Application.Interfaces;
using BarberiaReservas.Domain.Entities;
using BarberiaReservas.Domain.Interfaces;

namespace BarberiaReservas.Application.Services;

public class ServiceManager : IServiceManager
{
    private readonly IServiceRepository _serviceRepository;

    public ServiceManager(IServiceRepository serviceRepository)
    {
        _serviceRepository = serviceRepository;
    }

    public async Task<IEnumerable<Service>> GetAllServicesAsync()
    {
        return await _serviceRepository.GetAllAsync();
    }

    public async Task<Service?> GetServiceByIdAsync(int id)
    {
        return await _serviceRepository.GetByIdAsync(id);
    }

        public async Task<Service> CreateServiceAsync(Service service)
    {
        return await _serviceRepository.AddAsync(service);
    }

    public async Task UpdateServiceAsync(Service service)
    {
        await _serviceRepository.UpdateAsync(service);
    }

    public async Task DeleteServiceAsync(int id)
    {
        var service = await _serviceRepository.GetByIdAsync(id);

        if (service is null)
            throw new Exception("Servicio no encontrado.");

        await _serviceRepository.DeleteAsync(service);
    }
}