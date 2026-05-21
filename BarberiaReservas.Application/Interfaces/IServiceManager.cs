using BarberiaReservas.Application.DTOs;
using BarberiaReservas.Domain.Entities;

namespace BarberiaReservas.Application.Interfaces;

public interface IServiceManager
{
    Task<IEnumerable<Service>> GetAllAsync();
    Task<Service?> GetByIdAsync(int id);
    Task<Service> CreateAsync(CreateServiceDto dto);
    Task<bool> UpdateAsync(int id, UpdateServiceDto dto);
    Task<bool> DeleteAsync(int id);
}