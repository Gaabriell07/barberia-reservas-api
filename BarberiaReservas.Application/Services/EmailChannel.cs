using BarberiaReservas.Application.Interfaces;

namespace BarberiaReservas.Application.Services;

public class EmailChannel : INotificationChannel
{
    public string Name => "Email";

    public Task<bool> SendAsync(string recipient, string subject, string message)
    {
        return Task.FromResult(true);
    }
}