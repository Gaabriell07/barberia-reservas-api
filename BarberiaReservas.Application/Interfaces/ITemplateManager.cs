namespace BarberiaReservas.Application.Interfaces;

public interface ITemplateManager
{
    string Render(string templateKey, IReadOnlyDictionary<string, string>? variables = null);
}