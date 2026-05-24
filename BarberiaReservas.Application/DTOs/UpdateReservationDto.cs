using System;

namespace BarberiaReservas.Application.DTOs;

public class UpdateReservationDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ServiceId { get; set; }
    public DateTime DateTime { get; set; }
    public string? Notes { get; set; }
    public string Status { get; set; } = "Pending"; // Pending, Confirmed, Cancelled, Completed
}
