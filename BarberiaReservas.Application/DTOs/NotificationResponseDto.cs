namespace BarberiaReservas.Application.DTOs;

public class NotificationResponseDto
{
    public bool Success { get; set; }
    public string Channel { get; set; } = string.Empty;
    public string Recipient { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string? Error { get; set; }
}