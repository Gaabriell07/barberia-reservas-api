using BarberiaReservas.Domain.Entities;

namespace BarberiaReservas.Application.Interfaces;

public interface INotificationLogRepository
{
    Task AddAsync(NotificationLog log);
    Task<List<NotificationLog>> GetAllAsync();
}