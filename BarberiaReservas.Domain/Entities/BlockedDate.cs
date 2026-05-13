namespace BarberiaReservas.Domain.Entities;

public class BlockedDate
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Reason { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
