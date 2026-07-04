using BarberiaReservas.Application.Interfaces;
using BarberiaReservas.Domain.Entities;
using BarberiaReservas.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BarberiaReservas.Infrastructure.Repositories;

public class NotificationLogRepository : INotificationLogRepository
{
    private readonly AppDbContext _context;

    public NotificationLogRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(NotificationLog log)
    {
        _context.NotificationLogs.Add(log);
        await _context.SaveChangesAsync();
    }

    public async Task<List<NotificationLog>> GetAllAsync()
    {
        return await _context.NotificationLogs
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }
}