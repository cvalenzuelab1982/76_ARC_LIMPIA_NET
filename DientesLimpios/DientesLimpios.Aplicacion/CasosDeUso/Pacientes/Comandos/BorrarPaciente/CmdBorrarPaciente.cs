using DientesLimpios.Aplicacion.Utilidades.Mediador;

namespace DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.BorrarPaciente
{
    public class CmdBorrarPaciente : IRequest
    {
        public required Guid Id { get; set; }
    }
}
