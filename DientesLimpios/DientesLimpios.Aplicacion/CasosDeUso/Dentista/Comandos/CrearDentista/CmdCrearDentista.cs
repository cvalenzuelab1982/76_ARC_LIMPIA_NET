using DientesLimpios.Aplicacion.Utilidades.Mediador;

namespace DientesLimpios.Aplicacion.CasosDeUso.Dentista.Comandos.CrearDentista
{
    public class CmdCrearDentista : IRequest<Guid>
    {
        public required string Nombre { get; set; }
        public required string Email { get; set; }

    }
}
