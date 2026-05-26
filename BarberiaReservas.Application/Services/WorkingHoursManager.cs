using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarberiaReservas.Application.Interfaces;

namespace BarberiaReservas.Application.Services;

public class WorkingHoursManager : IWorkingHoursManager
{
    // Ejemplo: horario por defecto 09:00 - 18:00 y sin bloqueos.
    public Task<(TimeSpan Start, TimeSpan End)> GetWorkingHoursAsync(DateTime date)
    {
        return Task.FromResult((Start: new TimeSpan(9, 0, 0), End: new TimeSpan(18, 0, 0)));
    }

    public Task<IEnumerable<(DateTime Start, DateTime End)>> GetBlockedPeriodsAsync(DateTime date)
    {
        // No hay bloqueos por defecto
        return Task.FromResult(Enumerable.Empty<(DateTime Start, DateTime End)>());
    }
}
