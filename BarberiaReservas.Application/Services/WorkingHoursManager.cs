using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarberiaReservas.Application.Interfaces;
using BarberiaReservas.Domain.Interfaces;

namespace BarberiaReservas.Application.Services;

public class WorkingHoursManager : IWorkingHoursManager
{
    private readonly IWorkingHoursRepository _workingHoursRepository;

    public WorkingHoursManager(IWorkingHoursRepository workingHoursRepository)
    {
        _workingHoursRepository = workingHoursRepository;
    }

    public async Task<(TimeSpan Start, TimeSpan End)> GetWorkingHoursAsync(DateTime date)
    {
        var dayName = date.DayOfWeek.ToString();
        var workingHour = await _workingHoursRepository.GetByDayAsync(dayName);

        if (workingHour == null)
        {
            return (Start: new TimeSpan(9, 0, 0), End: new TimeSpan(18, 0, 0));
        }

        return (Start: workingHour.StartTime, End: workingHour.EndTime);
    }

    public async Task<IEnumerable<(DateTime Start, DateTime End)>> GetBlockedPeriodsAsync(DateTime date)
    {
        var blockedDates = await _workingHoursRepository.GetBlockedDatesAsync(date);

        return blockedDates.Select(b =>
            (Start: b.Date.Date.Add(new TimeSpan(0, 0, 0)),
             End: b.Date.Date.Add(new TimeSpan(23, 59, 59))));
    }
}
