using BarberiaReservas.Domain.Entities;

namespace BarberiaReservas.Domain.Interfaces;

public interface IReservationRepository
{
    Task<Reservation?> GetByIdAsync(int id);
    Task<IEnumerable<Reservation>> GetAllAsync();
    Task<IEnumerable<Reservation>> GetByUserIdAsync(int userId);
    Task<IEnumerable<Reservation>> GetByBarberIdAsync(int barberId);
    Task<Reservation> CreateAsync(Reservation reservation);
    Task<Reservation> UpdateAsync(Reservation reservation);
    Task<bool> DeleteAsync(int id);
}
