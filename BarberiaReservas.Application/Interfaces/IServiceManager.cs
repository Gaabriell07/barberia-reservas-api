using System.Collections.Generic;
using System.Threading.Tasks;
using BarberiaReservas.Domain.Entities;

namespace BarberiaReservas.Application.Interfaces;

public interface IServiceManager
{
    Task<IEnumerable<Service>> GetAllServicesAsync();
    Task<Service?> GetServiceByIdAsync(int id);
}
