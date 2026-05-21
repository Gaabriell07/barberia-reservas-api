namespace BarberiaReservas.Application.Interfaces;

public interface IPriceCalculator
{
    decimal CalculatePrice(decimal basePrice);
}