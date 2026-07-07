using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BarberiaReservas.Application.DTOs;

namespace BarberiaReservas.Application.Interfaces;

public interface IAvailabilityService
{
    Task<bool> IsTimeSlotAvailableAsync(DateTime dateTime, int durationMinutes, int barberId);
    Task<IEnumerable<TimeSlotDto>> GetAvailableSlotsAsync(DateTime date, int serviceId, int barberId);
}
