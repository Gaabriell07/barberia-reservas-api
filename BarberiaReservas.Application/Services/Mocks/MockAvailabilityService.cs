using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BarberiaReservas.Application.DTOs;
using BarberiaReservas.Application.Interfaces;

namespace BarberiaReservas.Application.Services.Mocks;

public class MockAvailabilityService : IAvailabilityService
{
    public async Task<bool> IsTimeSlotAvailableAsync(DateTime dateTime, int durationMinutes)
    {
        // Simulamos que el horario laboral es de 9 AM a 6 PM
        var isWorkingHour = dateTime.Hour >= 9 && dateTime.Hour < 18;
        return await Task.FromResult(isWorkingHour);
    }

    public async Task<IEnumerable<TimeSlotDto>> GetAvailableSlotsAsync(DateTime date, int serviceId)
    {
        // Retornamos un par de horarios hardcodeados realistas para las pruebas
        var slots = new List<TimeSlotDto>
        {
            new TimeSlotDto { StartTime = date.Date.AddHours(10), EndTime = date.Date.AddHours(10).AddMinutes(30), IsAvailable = true },
            new TimeSlotDto { StartTime = date.Date.AddHours(11), EndTime = date.Date.AddHours(11).AddMinutes(30), IsAvailable = true },
            new TimeSlotDto { StartTime = date.Date.AddHours(15), EndTime = date.Date.AddHours(15).AddMinutes(30), IsAvailable = true }
        };
        
        return await Task.FromResult(slots);
    }
}
