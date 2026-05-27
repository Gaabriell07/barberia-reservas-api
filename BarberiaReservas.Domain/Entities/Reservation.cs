namespace BarberiaReservas.Domain.Entities;

public class Reservation
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ServiceId { get; set; }
    public int BarberId { get; set; }
    public DateTime DateTime { get; set; }
    public string Status { get; set; } = "Pending"; 
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public User User { get; set; } = null!;
    public User Barber { get; set; } = null!;
    public Service Service { get; set; } = null!;
}
