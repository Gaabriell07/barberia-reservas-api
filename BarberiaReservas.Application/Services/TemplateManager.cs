using BarberiaReservas.Application.Interfaces;

namespace BarberiaReservas.Application.Services;

public class TemplateManager : ITemplateManager
{
    private static readonly Dictionary<string, string> Templates = new(StringComparer.OrdinalIgnoreCase)
    {
        ["ReservationCreated"] = "Hola {{ClientName}}, tu reserva para {{ServiceName}} fue creada para {{DateTime}}.",
        ["ReservationCancelled"] = "Hola {{ClientName}}, tu reserva para {{ServiceName}} fue cancelada."
    };

    public string Render(string templateKey, IReadOnlyDictionary<string, string>? variables = null)
    {
        if (!Templates.TryGetValue(templateKey, out var template))
            return string.Empty;

        var result = template;
        var safeVariables = variables ?? new Dictionary<string, string>();

        foreach (var pair in safeVariables)
        {
            result = result.Replace($"{{{{{pair.Key}}}}}", pair.Value, StringComparison.OrdinalIgnoreCase);
        }

        return result;
    }
}