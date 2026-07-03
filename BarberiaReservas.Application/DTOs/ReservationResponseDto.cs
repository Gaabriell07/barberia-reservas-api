using System;

namespace BarberiaReservas.Application.DTOs;

public class ReservationResponseDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;   
    public int ServiceId { get; set; }
    public string ServiceName { get; set; } = string.Empty; 
    public int BarberId { get; set; }
    public string BarberName { get; set; } = string.Empty;  
    public DateTime DateTime { get; set; }
    public string Status { get; set; } = "Pending";
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
