using BarberiaReservas.Application.DTOs;
using BarberiaReservas.Application.Interfaces;
using BarberiaReservas.Domain.Entities;
using BarberiaReservas.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BarberiaReservas.Infrastructure.Services;

public class ServiceManager : IServiceManager
{
    private readonly AppDbContext _context;

    public ServiceManager(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Service>> GetAllAsync()
    {
        return await _context.Services.ToListAsync();
    }

    public async Task<Service?> GetByIdAsync(int id)
    {
        return await _context.Services.FindAsync(id);
    }

    public async Task<Service> CreateAsync(CreateServiceDto dto)
    {
        var service = new Service
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            DurationMinutes = dto.DurationMinutes,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.Services.Add(service);
        await _context.SaveChangesAsync();

        return service;
    }

    public async Task<bool> UpdateAsync(int id, UpdateServiceDto dto)
    {
        var service = await _context.Services.FindAsync(id);

        if (service == null)
            return false;

        service.Name = dto.Name;
        service.Description = dto.Description;
        service.Price = dto.Price;
        service.DurationMinutes = dto.DurationMinutes;
        service.IsActive = dto.IsActive;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var service = await _context.Services.FindAsync(id);

        if (service == null)
            return false;

        _context.Services.Remove(service);

        await _context.SaveChangesAsync();

        return true;
    }
}