namespace BarberiaReservas.Domain.Entities;

public class WorkingHours
{
    public int Id { get; set; }
    public string DayOfWeek { get; set; } = string.Empty; 
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public bool IsActive { get; set; } = true;
}
