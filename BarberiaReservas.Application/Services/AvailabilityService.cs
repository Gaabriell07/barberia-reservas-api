using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarberiaReservas.Application.DTOs;
using BarberiaReservas.Application.Interfaces;
using BarberiaReservas.Domain.Interfaces;

namespace BarberiaReservas.Application.Services;

public class AvailabilityService : IAvailabilityService
{
    private readonly ITimeSlotGenerator _generator;
    private readonly IWorkingHoursManager _workingHoursManager;
    private readonly IReservationRepository _reservationRepository;

    public AvailabilityService(
        ITimeSlotGenerator generator, 
        IWorkingHoursManager workingHoursManager,
        IReservationRepository reservationRepository)
    {
        _generator = generator;
        _workingHoursManager = workingHoursManager;
        _reservationRepository = reservationRepository;
    }

    public async Task<bool> IsTimeSlotAvailableAsync(DateTime dateTime, int durationMinutes, int barberId)
    {
        var (start, end) = await _workingHoursManager.GetWorkingHoursAsync(dateTime.Date);
        var blocked = await _workingHoursManager.GetBlockedPeriodsAsync(dateTime.Date);

        var workStart = dateTime.Date.Add(start);
        var workEnd = dateTime.Date.Add(end);
        if (dateTime < workStart || dateTime.AddMinutes(durationMinutes) > workEnd)
            return false;

        var reservations = await _reservationRepository.GetByBarberIdAsync(barberId);
        var bookedSlots = reservations
            .Where(r => r.DateTime.Date == dateTime.Date && r.Status != "Cancelled")
            .Select(r => (r.DateTime, r.DateTime.AddMinutes(r.Service?.DurationMinutes ?? 30)))
            .ToList();

        var slots = await _generator.GenerateAsync(dateTime.Date, TimeSpan.FromMinutes(durationMinutes), start, end, blocked, bookedSlots);

        return slots.Any(s => s.StartTime == dateTime && s.IsAvailable);
    }

    public async Task<IEnumerable<TimeSlotDto>> GetAvailableSlotsAsync(DateTime date, int serviceId, int barberId)
    {
        
        var duration = TimeSpan.FromMinutes(30);

        var (start, end) = await _workingHoursManager.GetWorkingHoursAsync(date);
        var blocked = await _workingHoursManager.GetBlockedPeriodsAsync(date);

        var reservations = await _reservationRepository.GetByBarberIdAsync(barberId);
        var bookedSlots = reservations
            .Where(r => r.DateTime.Date == date.Date && r.Status != "Cancelled")
            .Select(r => (r.DateTime, r.DateTime.AddMinutes(r.Service?.DurationMinutes ?? 30)))
            .ToList();

        var slots = await _generator.GenerateAsync(date, duration, start, end, blocked, bookedSlots);

        return slots.Where(s => s.IsAvailable);
    }
}
