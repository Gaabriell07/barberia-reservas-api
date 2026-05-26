using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarberiaReservas.Application.DTOs;
using BarberiaReservas.Application.Interfaces;

namespace BarberiaReservas.Application.Services;

public class TimeSlotGenerator : ITimeSlotGenerator
{
    public Task<IEnumerable<TimeSlotDto>> GenerateAsync(DateTime date, TimeSpan slotDuration, TimeSpan workStart, TimeSpan workEnd, IEnumerable<(DateTime Start, DateTime End)> blockedPeriods, IEnumerable<(DateTime Start, DateTime End)> existingReservations, TimeSpan? step = null)
    {
        var result = new List<TimeSlotDto>();

        var current = date.Date.Add(workStart);
        var endOfDay = date.Date.Add(workEnd);
        var increment = step ?? slotDuration;

        while (current.Add(slotDuration) <= endOfDay)
        {
            var slotStart = current;
            var slotEnd = current.Add(slotDuration);

            bool overlapsBlocked = blockedPeriods.Any(b => b.Start < slotEnd && b.End > slotStart);
            bool overlapsReservation = existingReservations.Any(r => r.Start < slotEnd && r.End > slotStart);

            result.Add(new TimeSlotDto
            {
                StartTime = slotStart,
                EndTime = slotEnd,
                IsAvailable = !overlapsBlocked && !overlapsReservation
            });

            current = current.Add(increment);
        }

        return Task.FromResult((IEnumerable<TimeSlotDto>)result);
    }
}
