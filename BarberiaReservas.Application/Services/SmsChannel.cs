using BarberiaReservas.Application.Interfaces;

namespace BarberiaReservas.Application.Services;

public class SmsChannel : INotificationChannel
{
    public string Name => "Sms";

    public Task<bool> SendAsync(string recipient, string subject, string message)
    {
        return Task.FromResult(true);
    }
}