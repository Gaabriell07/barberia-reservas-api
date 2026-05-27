using BarberiaReservas.Application.Interfaces;

namespace BarberiaReservas.Infrastructure.Services;

public class PriceCalculator : IPriceCalculator
{
    public decimal CalculatePrice(decimal basePrice)
    {
        return basePrice;
    }
}