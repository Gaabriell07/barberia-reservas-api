using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarberiaReservas.Application.Interfaces;
using BarberiaReservas.Domain.Entities;

namespace BarberiaReservas.Application.Services.Mocks;

public class MockServiceManager : IServiceManager
{
    private readonly List<Service> _services = new List<Service>
    {
        new Service { Id = 1, Name = "Corte Clásico", Description = "Corte con tijera o máquina", Price = 15.00m, DurationMinutes = 30, IsActive = true, CreatedAt = DateTime.UtcNow },
        new Service { Id = 2, Name = "Arreglo de Barba", Description = "Perfilado y rebajado de barba", Price = 10.00m, DurationMinutes = 20, IsActive = true, CreatedAt = DateTime.UtcNow },
        new Service { Id = 3, Name = "Corte Premium + Barba", Description = "Corte completo, lavado y arreglo de barba", Price = 25.00m, DurationMinutes = 60, IsActive = true, CreatedAt = DateTime.UtcNow }
    };

    public async Task<IEnumerable<Service>> GetAllServicesAsync()
    {
        return await Task.FromResult(_services);
    }

    public async Task<Service?> GetServiceByIdAsync(int id)
    {
        var service = _services.FirstOrDefault(s => s.Id == id);
        return await Task.FromResult(service);
    }
}
