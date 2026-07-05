using BarberiaReservas.Application.Interfaces;

namespace BarberiaReservas.Application.Services;

public class SmsChannel : INotificationChannel
{
    public string Name => "Sms";

    public async Task<bool> SendAsync(string recipient, string subject, string message)
    {
        try
        {
            var outboxFolder = Path.Combine(AppContext.BaseDirectory, "sms-outbox");
            Directory.CreateDirectory(outboxFolder);

            var safeRecipient = string.Concat(recipient.Select(c =>
                Path.GetInvalidFileNameChars().Contains(c) ? '_' : c));

            var fileName = $"{DateTime.UtcNow:yyyyMMdd_HHmmssfff}_{safeRecipient}.txt";
            var filePath = Path.Combine(outboxFolder, fileName);

            var content =
                $"To: {recipient}\r\n" +
                $"Subject: {subject}\r\n" +
                $"Date: {DateTime.UtcNow:R}\r\n\r\n" +
                message;

            await File.WriteAllTextAsync(filePath, content);
            return true;
        }
        catch
        {
            return false;
        }
    }
}