using DientesLimpios.Aplicacion.Contratos.Identidad;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace DientesLimpios.Identidad.Servicios
{
    public class ServiciosUsuarios : IServiciosUsuarios
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ServiciosUsuarios(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string ObtenerUsuarioId()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)!;
        }
    }
}
