using System.Globalization;

namespace Zentica_frontend.Services;

// Ayudas para mostrar fechas, edades y estados en español
public static class Formato
{
    public static readonly CultureInfo Es = new("es-AR");

    public static string Fecha(DateTime f) => f.ToString("dd/MM/yyyy", Es);
    public static string Fecha(DateTime? f) => f.HasValue ? Fecha(f.Value) : "-";
    public static string Hora(DateTime f) => f.ToString("HH:mm", Es) + " hs";
    public static string FechaHora(DateTime f) => $"{Fecha(f)} - {Hora(f)}";
    public static string FechaLarga(DateTime f) => Mayus(f.ToString("dddd d 'de' MMMM", Es));
    public static string Mes(DateTime f) => Mayus(f.ToString("MMMM yyyy", Es));
    public static string Mayus(string s) => string.IsNullOrEmpty(s) ? s : char.ToUpper(s[0]) + s[1..];

    public static string Edad(DateTime nacimiento)
    {
        var hoy = DateTime.Today;
        var anios = hoy.Year - nacimiento.Year;
        if (nacimiento.Date > hoy.AddYears(-anios)) anios--;
        if (anios >= 1) return anios == 1 ? "1 año" : $"{anios} años";
        var meses = Math.Max(0, (hoy.Year - nacimiento.Year) * 12 + hoy.Month - nacimiento.Month - (hoy.Day < nacimiento.Day ? 1 : 0));
        return meses == 1 ? "1 mes" : $"{meses} meses";
    }

    // Horario de guardia: si termina antes de empezar, es nocturna (pasa al día siguiente)
    public static string Horario(TimeOnly inicio, TimeOnly fin) =>
        $"{inicio:HH\\:mm} a {fin:HH\\:mm} hs" + (fin < inicio ? " (día siguiente)" : "");

    public static string Peso(float kg) => kg.ToString("0.#", Es) + " kg";
    public static string Plata(decimal p) => p.ToString("C0", Es);

    // Clase CSS del "chip" de estado de un turno
    public static string ClaseEstado(string estado) => estado switch
    {
        "Aceptado" => "chip chip-verde",
        "Pendiente" => "chip chip-amarillo",
        "Atendido" => "chip chip-gris",
        _ => "chip chip-rosa" // Rechazado / Cancelado
    };
}
