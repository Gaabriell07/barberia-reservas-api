using System.Threading.Tasks;

namespace BarberiaReservas.Application.Interfaces;

public interface INotificationService
{
    Task SendReservationConfirmationAsync(int reservationId, string userEmail);
    Task SendCancellationNotificationAsync(int reservationId, string userEmail);
}
