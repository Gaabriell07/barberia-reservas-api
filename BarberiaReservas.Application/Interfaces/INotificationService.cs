using BarberiaReservas.Application.DTOs;

namespace BarberiaReservas.Application.Interfaces;

public interface INotificationService
{
    Task<NotificationResponseDto> SendAsync(NotificationDto dto);
}