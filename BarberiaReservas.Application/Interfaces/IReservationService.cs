using System.Collections.Generic;
using System.Threading.Tasks;
using BarberiaReservas.Application.DTOs;

namespace BarberiaReservas.Application.Interfaces;

public interface IReservationService
{
    Task<ReservationResponseDto> CreateReservationAsync(CreateReservationDto dto);
    Task<IEnumerable<ReservationResponseDto>> GetUserReservationsAsync(int userId);
    Task<bool> CancelReservationAsync(int reservationId);
}
