using BarberiaReservas.Application.DTOs;
using BarberiaReservas.Application.Interfaces;
using BarberiaReservas.Domain.Entities;

namespace BarberiaReservas.Application.Services;

public class NotificationService : INotificationService
{
    private readonly IReadOnlyDictionary<string, INotificationChannel> _channels;
    private readonly ITemplateManager _templateManager;
    private readonly INotificationLogRepository _logRepository;

    public NotificationService(
        IEnumerable<INotificationChannel> channels,
        ITemplateManager templateManager,
        INotificationLogRepository logRepository)
    {
        _channels = channels.ToDictionary(x => x.Name, StringComparer.OrdinalIgnoreCase);
        _templateManager = templateManager;
        _logRepository = logRepository;
    }

    public async Task<NotificationResponseDto> SendAsync(NotificationDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Recipient))
            return await FailAndLogAsync(dto, "El destinatario es obligatorio.");

        if (!_channels.TryGetValue(dto.Channel, out var channel))
            return await FailAndLogAsync(dto, $"Canal no soportado: {dto.Channel}");

        var finalMessage = dto.Message;

        if (!string.IsNullOrWhiteSpace(dto.TemplateKey))
        {
            var rendered = _templateManager.Render(dto.TemplateKey, dto.Variables);
            if (!string.IsNullOrWhiteSpace(rendered))
                finalMessage = rendered;
        }

        if (string.IsNullOrWhiteSpace(finalMessage))
            return await FailAndLogAsync(dto, "El mensaje no puede estar vacío.");

        bool sent;
        string? error = null;

        try
        {
            sent = await channel.SendAsync(dto.Recipient, dto.Subject, finalMessage);
            if (!sent)
                error = "No se pudo enviar la notificación.";
        }
        catch (Exception ex)
        {
            sent = false;
            error = ex.Message;
        }

        await _logRepository.AddAsync(new NotificationLog
        {
            Channel = channel.Name,
            Recipient = dto.Recipient,
            Subject = dto.Subject,
            Message = finalMessage,
            Success = sent,
            Error = error
        });

        return new NotificationResponseDto
        {
            Success = sent,
            Channel = channel.Name,
            Recipient = dto.Recipient,
            Message = finalMessage,
            Error = error
        };
    }

    private async Task<NotificationResponseDto> FailAndLogAsync(NotificationDto dto, string error)
    {
        await _logRepository.AddAsync(new NotificationLog
        {
            Channel = dto.Channel,
            Recipient = dto.Recipient,
            Subject = dto.Subject,
            Message = dto.Message,
            Success = false,
            Error = error
        });

        return new NotificationResponseDto
        {
            Success = false,
            Channel = dto.Channel,
            Recipient = dto.Recipient,
            Message = dto.Message,
            Error = error
        };
    }
}