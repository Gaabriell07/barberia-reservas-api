namespace BarberiaReservas.Application.DTOs;

public class NotificationDto
{
    public string Channel { get; set; } = "Email";
    public string Recipient { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string? TemplateKey { get; set; }
    public Dictionary<string, string> Variables { get; set; } = new();
}