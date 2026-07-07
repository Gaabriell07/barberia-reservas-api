using BarberiaReservas.Domain.Entities;

namespace BarberiaReservas.Domain.Interfaces;

public interface IWorkingHoursRepository
{
    Task<WorkingHours?> GetByDayAsync(string dayOfWeek);
    Task<IEnumerable<BlockedDate>> GetBlockedDatesAsync(DateTime date);
}
