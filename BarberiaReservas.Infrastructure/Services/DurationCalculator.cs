using BarberiaReservas.Application.Interfaces;

namespace BarberiaReservas.Infrastructure.Services;

public class DurationCalculator : IDurationCalculator
{
    public int CalculateDuration(int minutes)
    {
        return minutes;
    }
}