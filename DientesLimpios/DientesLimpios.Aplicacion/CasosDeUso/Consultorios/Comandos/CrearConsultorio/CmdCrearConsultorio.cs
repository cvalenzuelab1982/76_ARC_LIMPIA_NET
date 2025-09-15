using DientesLimpios.Aplicacion.Utilidades.Mediador;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio
{
    public class CmdCrearConsultorio : IRequest<Guid>
    {
        public required string Nombre { get; set; }
    }
}
