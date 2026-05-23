namespace BarberiaReservas.Application.Interfaces;

public interface INotificationChannel
{
    string Name { get; }
    Task<bool> SendAsync(string recipient, string subject, string message);
}