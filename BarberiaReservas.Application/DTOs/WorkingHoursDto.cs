using System;

namespace BarberiaReservas.Application.DTOs;

public class WorkingHoursResponseDto
{
    public int Id { get; set; }
    public string DayOfWeek { get; set; } = string.Empty;
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public bool IsActive { get; set; }
}

public class UpdateWorkingHoursDto
{
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public bool IsActive { get; set; }
}

public class BlockedDateResponseDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Reason { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

public class CreateBlockedDateDto
{
    public DateTime Date { get; set; }
    public string Reason { get; set; } = string.Empty;
}
