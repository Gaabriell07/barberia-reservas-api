using System;
using System.Threading.Tasks;
using BarberiaReservas.Application.Interfaces;

namespace BarberiaReservas.Application.Services.Mocks;

public class MockNotificationService : INotificationService
{
    public Task SendReservationConfirmationAsync(int reservationId, string userEmail)
    {
        Console.WriteLine($"[MOCK EMAIL] Confirmación enviada a {userEmail} para la reserva #{reservationId}");
        return Task.CompletedTask;
    }

    public Task SendCancellationNotificationAsync(int reservationId, string userEmail)
    {
        Console.WriteLine($"[MOCK EMAIL] Cancelación enviada a {userEmail} para la reserva #{reservationId}");
        return Task.CompletedTask;
    }
}
