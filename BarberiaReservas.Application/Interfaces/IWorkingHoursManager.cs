using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BarberiaReservas.Application.Interfaces;

public interface IWorkingHoursManager
{
    Task<(TimeSpan Start, TimeSpan End)> GetWorkingHoursAsync(DateTime date);
    Task<IEnumerable<(DateTime Start, DateTime End)>> GetBlockedPeriodsAsync(DateTime date);
}
