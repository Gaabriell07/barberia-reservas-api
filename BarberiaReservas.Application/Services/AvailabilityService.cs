using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarberiaReservas.Application.DTOs;
using BarberiaReservas.Application.Interfaces;

namespace BarberiaReservas.Application.Services;

public class AvailabilityService : IAvailabilityService
{
    private readonly ITimeSlotGenerator _generator;
    private readonly IWorkingHoursManager _workingHoursManager;

    public AvailabilityService(ITimeSlotGenerator generator, IWorkingHoursManager workingHoursManager)
    {
        _generator = generator;
        _workingHoursManager = workingHoursManager;
    }

    public async Task<bool> IsTimeSlotAvailableAsync(DateTime dateTime, int durationMinutes)
    {
        var (start, end) = await _workingHoursManager.GetWorkingHoursAsync(dateTime.Date);
        var blocked = await _workingHoursManager.GetBlockedPeriodsAsync(dateTime.Date);

        // Si fuera fuera de horario laboral
        var workStart = dateTime.Date.Add(start);
        var workEnd = dateTime.Date.Add(end);
        if (dateTime < workStart || dateTime.AddMinutes(durationMinutes) > workEnd)
            return false;

        var slots = await _generator.GenerateAsync(dateTime.Date, TimeSpan.FromMinutes(durationMinutes), start, end, blocked, Enumerable.Empty<(DateTime, DateTime)>());

        return slots.Any(s => s.StartTime == dateTime && s.IsAvailable);
    }

    public async Task<IEnumerable<TimeSlotDto>> GetAvailableSlotsAsync(DateTime date, int serviceId)
    {
        // Por ahora el serviceId solo define la duración del servicio; se puede mapear más tarde.
        var duration = TimeSpan.FromMinutes(30);

        var (start, end) = await _workingHoursManager.GetWorkingHoursAsync(date);
        var blocked = await _workingHoursManager.GetBlockedPeriodsAsync(date);

        var slots = await _generator.GenerateAsync(date, duration, start, end, blocked, Enumerable.Empty<(DateTime, DateTime)>());

        return slots.Where(s => s.IsAvailable);
    }
}
