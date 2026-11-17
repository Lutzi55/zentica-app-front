namespace Zentica_frontend.Services;

// Links para llamar o escribir por WhatsApp a partir de un teléfono
public static class Contacto
{
    public static string Digitos(string? telefono) => new((telefono ?? "").Where(char.IsDigit).ToArray());
    public static bool TieneTelefono(string? telefono) => Digitos(telefono).Length >= 8;
    public static string Llamar(string? telefono) => "tel:+" + Digitos(telefono);
    public static string WhatsApp(string? telefono) => "https://wa.me/" + Digitos(telefono);
}
