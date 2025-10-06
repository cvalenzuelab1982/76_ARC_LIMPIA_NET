using DientesLimpios.Aplicacion.Utilidades.Mediador;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.BorrarConsultorio
{
    public class CmdBorrarConsultorio : IRequest
    {
        public Guid Id { get; set; }
    }
}
