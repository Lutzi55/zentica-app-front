namespace Zentica_frontend.Models;

// Copias de los DTOs de la API (mismos nombres de propiedades)

public class Sesion
{
    public string Token { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Rol { get; set; } = string.Empty;
    public int IdUsuario { get; set; }
    public int? IdDoctor { get; set; }
    public int? IdDuenio { get; set; }
    public string? Mensaje { get; set; }
}

public class Rol
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
}

public class Usuario
{
    public int IdUsuario { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Telefono { get; set; }
    public int IdRol { get; set; }
    public string Rol { get; set; } = string.Empty;
    public bool Activo { get; set; }
    public string? Dni { get; set; }
    public string? Direccion { get; set; }
    public string? Zona { get; set; }
    public int? IdDoctor { get; set; }
    public string? Matricula { get; set; }
    public string? Cargo { get; set; }
    public List<string> Mascotas { get; set; } = new();
}

public class UsuarioForm
{
    public string Nombre { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Password { get; set; }
    public string? Telefono { get; set; }
    public int IdRol { get; set; }
    public string? Dni { get; set; }
    public string? Direccion { get; set; }
    public string? Zona { get; set; }
    public string? Matricula { get; set; }
    public string? Cargo { get; set; }
}

public class Veterinario
{
    public int IdDoctor { get; set; }
    public int IdUsuario { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Matricula { get; set; } = string.Empty;
    public string Cargo { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Telefono { get; set; }
    public bool Activo { get; set; }
    public string NombreCompleto => $"{Nombre} {Apellido}".Trim();
}

public class VeterinarioForm
{
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Matricula { get; set; } = string.Empty;
    public string Cargo { get; set; } = "Veterinario/a";
    public string Email { get; set; } = string.Empty;
    public string? Telefono { get; set; }
    public string? Password { get; set; }
}

public class Raza
{
    public int IdRaza { get; set; }
    public string Nombre { get; set; } = string.Empty;
}

public class Especie
{
    public int IdEspecie { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public List<Raza> Razas { get; set; } = new();
}

public class Mascota
{
    public int IdMascota { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public DateTime FechaNacimiento { get; set; }
    public string? Sexo { get; set; }
    public float Peso { get; set; }
    public string? Color { get; set; }
    public int IdRaza { get; set; }
    public string Raza { get; set; } = string.Empty;
    public int IdEspecie { get; set; }
    public string Especie { get; set; } = string.Empty;
    public int IdUsuario { get; set; }
    public string Duenio { get; set; } = string.Empty;
    public string? DuenioTelefono { get; set; }
    public string? DuenioDni { get; set; }
    public bool VacunaPendiente { get; set; }
    public string? Foto { get; set; }
}

public class Horario
{
    public string Hora { get; set; } = string.Empty;
    public bool Disponible { get; set; }
}

public class MascotaForm
{
    public string Nombre { get; set; } = string.Empty;
    public DateTime FechaNacimiento { get; set; } = DateTime.Today.AddYears(-1);
    public string? Sexo { get; set; } = "Macho";
    public float Peso { get; set; }
    public string? Color { get; set; }
    public int IdRaza { get; set; }
    public int IdUsuario { get; set; }
}

public class Turno
{
    public int IdTurno { get; set; }
    public DateTime FechaYHora { get; set; }
    public bool EsUrgencia { get; set; }
    public string Estado { get; set; } = string.Empty;
    public string? Motivo { get; set; }
    public int IdDoctor { get; set; }
    public string Doctor { get; set; } = string.Empty;
    public int IdMascota { get; set; }
    public string Mascota { get; set; } = string.Empty;
    public string Raza { get; set; } = string.Empty;
    public string Especie { get; set; } = string.Empty;
    public int IdUsuarioDuenio { get; set; }
    public string Duenio { get; set; } = string.Empty;
}

public class TurnoForm
{
    public DateTime FechaYHora { get; set; }
    public bool EsUrgencia { get; set; }
    public string? Motivo { get; set; }
    public int IdDoctor { get; set; }
    public int IdMascota { get; set; }
}

public class Guardia
{
    public int IdGuardia { get; set; }
    public DateTime Fecha { get; set; }
    public TimeOnly HoraInicio { get; set; }
    public TimeOnly HoraFin { get; set; }
    public string? Observaciones { get; set; }
    public int IdDoctor { get; set; }
    public string Doctor { get; set; } = string.Empty;
}

public class GuardiaForm
{
    public DateTime Fecha { get; set; }
    public TimeOnly HoraInicio { get; set; } = new(8, 0);
    public TimeOnly HoraFin { get; set; } = new(20, 0);
    public string? Observaciones { get; set; }
    public int IdDoctor { get; set; }
}

public class Vacuna
{
    public int IdVacuna { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public DateTime FechaAplicacion { get; set; }
    public DateTime? FechaProxima { get; set; }
    public int IdMascota { get; set; }
    public string Doctor { get; set; } = string.Empty;
}

public class VacunaForm
{
    public string Nombre { get; set; } = string.Empty;
    public DateTime FechaAplicacion { get; set; } = DateTime.Today;
    public DateTime? FechaProxima { get; set; }
    public int IdMascota { get; set; }
    public int IdDoctor { get; set; }
}

public class Receta
{
    public int IdReceta { get; set; }
    public string Medicamento { get; set; } = string.Empty;
    public DateTime FechaEmision { get; set; }
    public DateTime? FechaVencimiento { get; set; }
    public string Doctor { get; set; } = string.Empty;
}

public class RecetaForm
{
    public string Medicamento { get; set; } = string.Empty;
    public DateTime? FechaVencimiento { get; set; }
}

public class Consulta
{
    public int IdConsulta { get; set; }
    public DateTime Fecha { get; set; }
    public string? Motivo { get; set; }
    public string? Diagnostico { get; set; }
    public decimal Costo { get; set; }
    public string Estado { get; set; } = string.Empty;
    public int IdMascota { get; set; }
    public string Mascota { get; set; } = string.Empty;
    public string Doctor { get; set; } = string.Empty;
}

public class ConsultaForm
{
    public int IdTurno { get; set; }
    public string Diagnostico { get; set; } = string.Empty;
    public string? Tratamiento { get; set; }
    public decimal Costo { get; set; }
    public List<VacunaForm> Vacunas { get; set; } = new();
    public List<RecetaForm> Recetas { get; set; } = new();
}

public class ResumenAdmin
{
    public int Usuarios { get; set; }
    public int Veterinarios { get; set; }
    public int Mascotas { get; set; }
    public int TurnosHoy { get; set; }
    public int GuardiasMes { get; set; }
}

public class MensajeChat
{
    public int IdMensaje { get; set; }
    public string Texto { get; set; } = string.Empty;
    public DateTime FechaEnvio { get; set; }
    public bool Leido { get; set; }
    public bool EsMio { get; set; }
}

public class Conversacion
{
    public int IdUsuario { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Rol { get; set; } = string.Empty;
    public string? Detalle { get; set; }
    public string? Telefono { get; set; }
    public string? UltimoMensaje { get; set; }
    public DateTime? FechaUltimo { get; set; }
    public int NoLeidos { get; set; }
}
