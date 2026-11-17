using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Zentica_frontend.Models;

namespace Zentica_frontend.Services;

// Resultado de un POST/PUT/PATCH/DELETE
public record Respuesta(bool Ok, string? Error = null);

// Centraliza todas las llamadas a la API y guarda la sesión (token + rol).
public class ApiService
{
    private readonly HttpClient _http;

    public ApiService(HttpClient http) => _http = http;

    public Sesion? Sesion { get; private set; }
    public bool EstaLogueado => !string.IsNullOrEmpty(Sesion?.Token);
    public string Rol => Sesion?.Rol ?? string.Empty;
    public bool EsAdmin => Rol == "Administrador";
    public bool EsVeterinario => Rol == "Veterinario";
    public bool EsDuenio => Rol == "Dueño";

    // Mensaje del último GET que falló (para mostrar en pantalla)
    public string? UltimoError { get; private set; }

    // Pantalla de inicio según el rol
    public string RutaInicio => Rol switch
    {
        "Administrador" => "/admin/home",
        "Veterinario" => "/doctor/home",
        "Dueño" => "/home",
        _ => "/"
    };

    public async Task<Respuesta> LoginAsync(string email, string password)
    {
        try
        {
            var r = await _http.PostAsJsonAsync("api/Auth/login", new { email, password });
            if (!r.IsSuccessStatusCode) return new Respuesta(false, await LeerErrorAsync(r));

            Sesion = await r.Content.ReadFromJsonAsync<Sesion>();
            return EstaLogueado ? new Respuesta(true) : new Respuesta(false, "Respuesta inválida de la API.");
        }
        catch (Exception ex)
        {
            return new Respuesta(false, ErrorConexion(ex));
        }
    }

    public void CerrarSesion() => Sesion = null;

    public async Task<T?> GetAsync<T>(string ruta)
    {
        UltimoError = null;
        try
        {
            var r = await _http.SendAsync(Armar(HttpMethod.Get, ruta));
            if (!r.IsSuccessStatusCode)
            {
                UltimoError = await LeerErrorAsync(r);
                return default;
            }
            return await r.Content.ReadFromJsonAsync<T>();
        }
        catch (Exception ex)
        {
            UltimoError = ErrorConexion(ex);
            return default;
        }
    }

    // GET de listas: nunca devuelve null
    public async Task<List<T>> GetListaAsync<T>(string ruta) => await GetAsync<List<T>>(ruta) ?? new List<T>();

    public Task<Respuesta> PostAsync<T>(string ruta, T cuerpo) => EnviarAsync(HttpMethod.Post, ruta, cuerpo);
    public Task<Respuesta> PutAsync<T>(string ruta, T cuerpo) => EnviarAsync(HttpMethod.Put, ruta, cuerpo);
    public Task<Respuesta> PatchAsync<T>(string ruta, T cuerpo) => EnviarAsync(HttpMethod.Patch, ruta, cuerpo);
    public Task<Respuesta> DeleteAsync(string ruta) => EnviarAsync<object?>(HttpMethod.Delete, ruta, null);

    private async Task<Respuesta> EnviarAsync<T>(HttpMethod metodo, string ruta, T cuerpo)
    {
        try
        {
            var req = Armar(metodo, ruta);
            if (cuerpo != null) req.Content = JsonContent.Create(cuerpo);
            var r = await _http.SendAsync(req);
            return r.IsSuccessStatusCode ? new Respuesta(true) : new Respuesta(false, await LeerErrorAsync(r));
        }
        catch (Exception ex)
        {
            return new Respuesta(false, ErrorConexion(ex));
        }
    }

    private HttpRequestMessage Armar(HttpMethod metodo, string ruta)
    {
        var req = new HttpRequestMessage(metodo, ruta);
        if (EstaLogueado)
            req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Sesion!.Token);
        return req;
    }

    // Saca el texto del error que manda la API: { mensaje } o los errores de validación
    private static async Task<string> LeerErrorAsync(HttpResponseMessage r)
    {
        if (r.StatusCode == HttpStatusCode.Unauthorized && r.RequestMessage?.RequestUri?.AbsolutePath.Contains("login") != true)
            return "Tu sesión venció. Cerrá sesión y volvé a ingresar.";
        if (r.StatusCode == HttpStatusCode.Forbidden)
            return "No tenés permiso para hacer esto.";

        try
        {
            var texto = await r.Content.ReadAsStringAsync();
            if (!string.IsNullOrWhiteSpace(texto))
            {
                using var doc = JsonDocument.Parse(texto);
                var raiz = doc.RootElement;
                if (raiz.ValueKind == JsonValueKind.Object)
                {
                    if (raiz.TryGetProperty("mensaje", out var m)) return m.GetString() ?? "Error.";
                    if (raiz.TryGetProperty("errors", out var errs) && errs.ValueKind == JsonValueKind.Object)
                    {
                        var lista = errs.EnumerateObject().SelectMany(p => p.Value.EnumerateArray())
                            .Select(e => e.GetString()).Where(e => !string.IsNullOrEmpty(e));
                        return string.Join(" ", lista);
                    }
                    if (raiz.TryGetProperty("title", out var t)) return t.GetString() ?? "Error.";
                }
            }
        }
        catch (JsonException) { }

        return $"Error {(int)r.StatusCode} al comunicarse con la API.";
    }

    private static string ErrorConexion(Exception ex) => ex switch
    {
        TaskCanceledException => "La API tardó demasiado en responder.",
        HttpRequestException => "No se pudo conectar con la API. ¿Está encendida?",
        _ => "Error: " + ex.Message
    };
}
