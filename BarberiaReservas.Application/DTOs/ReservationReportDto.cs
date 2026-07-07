namespace BarberiaReservas.Application.DTOs;

public class ReservationReportDto
{
    public int TotalReservations { get; set; }
    public int CompletedReservations { get; set; }
    public int CancelledReservations { get; set; }
    public decimal EstimatedRevenue { get; set; }
}
