# Zéntica App — Clínica & Veterinaria

App móvil del sistema Zéntica, hecha en .NET MAUI Blazor Hybrid (Android y Windows). Cada usuario inicia sesión y entra a la pantalla de su rol.

> La API que usa esta app está en el repositorio **Zentica_Api** (ahí también están el DER y el diagrama de casos de uso).

## Tecnologías

.NET MAUI Blazor Hybrid · .NET 8 · Bootstrap 5 · Bootstrap Icons

## Pantallas por rol

- **Administrador:** resumen, gestión de usuarios, gestión de veterinarios y asignación de guardias.
- **Veterinario:** turnos para aceptar o rechazar, agenda, atender cita, registrar diagnóstico (vacunas y recetas), historial clínico, pacientes, alta de mascotas, días de guardia y mensajes.
- **Dueño:** mis mascotas (vacunas, historial y foto), mis turnos, pedir turno con horarios libres, veterinario de guardia y chat con el veterinario.

## Usuarios de prueba

| Rol | Email | Contraseña |
|---|---|---|
| Administrador | admin@zentica.com | Admin123 |
| Veterinario | roberto@zentica.com | Vet12345 |
| Dueña | sofia@zentica.com | Dueno123 |

## Cómo ejecutar

1. Abrir `Zentica_frontend.sln` en Visual Studio 2022 (con la carga de trabajo MAUI).
2. En `MauiProgram.cs`: `UsarHosting = true` usa la API publicada (`http://zentica.runasp.net`); `false` usa la API local.
3. **Windows:** destino *Windows Machine*.
4. **Celular:** depuración USB activada, destino *net8.0-android → dispositivo*.
5. **APK:** `dotnet publish -f net8.0-android -c Release` → `bin\Release\net8.0-android\publish\*-Signed.apk`.
