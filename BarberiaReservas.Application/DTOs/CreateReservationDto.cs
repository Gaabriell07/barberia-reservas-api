using System;

namespace BarberiaReservas.Application.DTOs;

public class CreateReservationDto
{
    public int UserId { get; set; }
    public int ServiceId { get; set; }
    public int BarberId { get; set; }
    public DateTime DateTime { get; set; }
    public string? Notes { get; set; }
}
