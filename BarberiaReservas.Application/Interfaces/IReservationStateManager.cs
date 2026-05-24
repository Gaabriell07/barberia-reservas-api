namespace BarberiaReservas.Application.Interfaces;

public interface IReservationStateManager
{
    Task<bool> ConfirmReservationAsync(int reservationId);
    Task<bool> CancelReservationAsync(int reservationId);
    Task<bool> CompleteReservationAsync(int reservationId);
    Task<string> GetReservationStatusAsync(int reservationId);
}
