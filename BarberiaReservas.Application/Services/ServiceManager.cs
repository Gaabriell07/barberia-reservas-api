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
}