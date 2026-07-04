using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarberiaReservas.Application.Interfaces;
using BarberiaReservas.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BarberiaReservas.Application.Services;

public class WorkingHoursManager : IWorkingHoursManager
{
    private readonly AppDbContext _context;

    public WorkingHoursManager(AppDbContext context)
    {
        _context = context;
    }

    public async Task<(TimeSpan Start, TimeSpan End)> GetWorkingHoursAsync(DateTime date)
    {
        var dayName = date.DayOfWeek.ToString();

        var workingHour = await _context.WorkingHours
            .AsNoTracking()
            .FirstOrDefaultAsync(w => w.DayOfWeek == dayName && w.IsActive);

        if (workingHour == null)
        {
            return (Start: new TimeSpan(9, 0, 0), End: new TimeSpan(18, 0, 0));
        }

        return (Start: workingHour.StartTime, End: workingHour.EndTime);
    }

    public async Task<IEnumerable<(DateTime Start, DateTime End)>> GetBlockedPeriodsAsync(DateTime date)
    {
        var blockedDates = await _context.BlockedDates
            .AsNoTracking()
            .Where(b => b.Date.Date == date.Date)
            .ToListAsync();

        return blockedDates.Select(b => 
            (Start: b.Date.Date.Add(new TimeSpan(0, 0, 0)), 
             End: b.Date.Date.Add(new TimeSpan(23, 59, 59))));
    }
}
