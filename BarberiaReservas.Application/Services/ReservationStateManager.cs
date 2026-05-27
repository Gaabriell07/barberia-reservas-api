using BarberiaReservas.Application.Interfaces;

namespace BarberiaReservas.Application.Services;

public class ReservationStateManager : IReservationStateManager
{
    
    private static readonly Dictionary<int, string> _reservationStates = new();

    public async Task<bool> ConfirmReservationAsync(int reservationId)
    {
        try
        {
            if (reservationId <= 0)
                return false;

            _reservationStates[reservationId] = "Confirmed";
            return await Task.FromResult(true);
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> CancelReservationAsync(int reservationId)
    {
        try
        {
            if (reservationId <= 0)
                return false;

            _reservationStates[reservationId] = "Cancelled";
            return await Task.FromResult(true);
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> CompleteReservationAsync(int reservationId)
    {
        try
        {
            if (reservationId <= 0)
                return false;

            _reservationStates[reservationId] = "Completed";
            return await Task.FromResult(true);
        }
        catch
        {
            return false;
        }
    }

    public async Task<string> GetReservationStatusAsync(int reservationId)
    {
        try
        {
            if (_reservationStates.TryGetValue(reservationId, out var status))
                return await Task.FromResult(status);

            return await Task.FromResult("Pending");
        }
        catch
        {
            return "Unknown";
        }
    }
}
