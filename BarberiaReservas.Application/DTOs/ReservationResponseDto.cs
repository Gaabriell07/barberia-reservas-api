using System;

namespace BarberiaReservas.Application.DTOs;

public class ReservationResponseDto
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string ServiceName { get; set; } = string.Empty;
    public DateTime DateTime { get; set; }
    public string Status { get; set; } = string.Empty;
}
