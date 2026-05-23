using BarberiaReservas.Application.DTOs;
using BarberiaReservas.Application.Interfaces;

namespace BarberiaReservas.Application.Services;

public class NotificationService : INotificationService
{
    private readonly IReadOnlyDictionary<string, INotificationChannel> _channels;
    private readonly ITemplateManager _templateManager;

    public NotificationService(IEnumerable<INotificationChannel> channels, ITemplateManager templateManager)
    {
        _channels = channels.ToDictionary(x => x.Name, StringComparer.OrdinalIgnoreCase);
        _templateManager = templateManager;
    }

    public async Task<NotificationResponseDto> SendAsync(NotificationDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Recipient))
        {
            return new NotificationResponseDto
            {
                Success = false,
                Error = "El destinatario es obligatorio."
            };
        }

        if (!_channels.TryGetValue(dto.Channel, out var channel))
        {
            return new NotificationResponseDto
            {
                Success = false,
                Error = $"Canal no soportado: {dto.Channel}"
            };
        }

        var finalMessage = dto.Message;

        if (!string.IsNullOrWhiteSpace(dto.TemplateKey))
        {
            var rendered = _templateManager.Render(dto.TemplateKey, dto.Variables);
            if (!string.IsNullOrWhiteSpace(rendered))
            {
                finalMessage = rendered;
            }
        }

        if (string.IsNullOrWhiteSpace(finalMessage))
        {
            return new NotificationResponseDto
            {
                Success = false,
                Error = "El mensaje no puede estar vacío."
            };
        }

        var sent = await channel.SendAsync(dto.Recipient, dto.Subject, finalMessage);

        return new NotificationResponseDto
        {
            Success = sent,
            Channel = channel.Name,
            Recipient = dto.Recipient,
            Message = finalMessage,
            Error = sent ? null : "No se pudo enviar la notificación."
        };
    }
}