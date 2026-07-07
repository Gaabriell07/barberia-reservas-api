using BarberiaReservas.Application.DTOs;

namespace BarberiaReservas.Application.Interfaces;

public interface IReservationService
{
    Task<ReservationResponseDto> GetReservationAsync(int id);
    Task<IEnumerable<ReservationResponseDto>> GetUserReservationsAsync(int userId);
    Task<IEnumerable<ReservationResponseDto>> GetAllReservationsAsync();
    Task<ReservationResponseDto> CreateReservationAsync(CreateReservationDto dto);
    Task<ReservationResponseDto> UpdateReservationAsync(int id, UpdateReservationDto dto);
    Task<bool> CancelReservationAsync(int reservationId);
    Task<ReservationReportDto> GetReservationReportAsync();
}
