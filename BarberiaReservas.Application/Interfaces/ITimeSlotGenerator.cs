using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BarberiaReservas.Application.DTOs;

namespace BarberiaReservas.Application.Interfaces;

public interface ITimeSlotGenerator
{
    Task<IEnumerable<TimeSlotDto>> GenerateAsync(
        DateTime date,
        TimeSpan slotDuration,
        TimeSpan workStart,
        TimeSpan workEnd,
        IEnumerable<(DateTime Start, DateTime End)> blockedPeriods,
        IEnumerable<(DateTime Start, DateTime End)> existingReservations,
        TimeSpan? step = null
    );
}
