using Microsoft.AspNetCore.Components;
using Zentica_frontend.Services;

namespace Zentica_frontend.Components.Layout;

// Base de los layouts con sesión: si no hay login vuelve al Login,
// y si el rol no corresponde manda al inicio de su rol.
public abstract class LayoutBase : LayoutComponentBase
{
    [Inject] protected ApiService Api { get; set; } = default!;
    [Inject] protected NavigationManager Nav { get; set; } = default!;

    protected abstract string RolPermitido { get; }
    protected bool Autorizado { get; private set; }

    protected override void OnParametersSet()
    {
        if (!Api.EstaLogueado) { Autorizado = false; Nav.NavigateTo("/login"); return; }
        if (Api.Rol != RolPermitido) { Autorizado = false; Nav.NavigateTo(Api.RutaInicio); return; }
        Autorizado = true;
    }
}
