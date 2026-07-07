using BarberiaReservas.Domain.Entities;
using BarberiaReservas.Domain.Interfaces;
using BarberiaReservas.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BarberiaReservas.Infrastructure.Repositories;

public class WorkingHoursRepository : IWorkingHoursRepository
{
    private readonly AppDbContext _context;

    public WorkingHoursRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<WorkingHours?> GetByDayAsync(string dayOfWeek)
    {
        return await _context.WorkingHours
            .AsNoTracking()
            .FirstOrDefaultAsync(w => w.DayOfWeek == dayOfWeek && w.IsActive);
    }

    public async Task<IEnumerable<BlockedDate>> GetBlockedDatesAsync(DateTime date)
    {
        return await _context.BlockedDates
            .AsNoTracking()
            .Where(b => b.Date.Date == date.Date)
            .OrderBy(b => b.Date)
            .ToListAsync();
    }
}
