using BarberiaReservas.Application.DTOs;
using BarberiaReservas.Application.Interfaces;
using BarberiaReservas.Domain.Entities;

namespace BarberiaReservas.Application.Services.Mocks;

public class MockServiceManager : IServiceManager
{
    private readonly List<Service> _services = new();

    public Task<IEnumerable<Service>> GetAllAsync()
    {
        return Task.FromResult(_services.AsEnumerable());
    }

    public Task<Service?> GetByIdAsync(int id)
    {
        var service = _services.FirstOrDefault(s => s.Id == id);
        return Task.FromResult(service);
    }

    public Task<Service> CreateAsync(CreateServiceDto dto)
    {
        var service = new Service
        {
            Id = _services.Count + 1,
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            DurationMinutes = dto.DurationMinutes,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _services.Add(service);

        return Task.FromResult(service);
    }

    public Task<bool> UpdateAsync(int id, UpdateServiceDto dto)
    {
        var service = _services.FirstOrDefault(s => s.Id == id);

        if (service == null)
            return Task.FromResult(false);

        service.Name = dto.Name;
        service.Description = dto.Description;
        service.Price = dto.Price;
        service.DurationMinutes = dto.DurationMinutes;
        service.IsActive = dto.IsActive;

        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var service = _services.FirstOrDefault(s => s.Id == id);

        if (service == null)
            return Task.FromResult(false);

        _services.Remove(service);

        return Task.FromResult(true);
    }
}