using DientesLimpios.Aplicacion.Utilidades.Mediador;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.ActualizarConsultorio
{
    public class CmdActualizarConsultorio : IRequest
    {
        public Guid Id { get; set; }
        public required string Nombre { get; set; }
    }
}
