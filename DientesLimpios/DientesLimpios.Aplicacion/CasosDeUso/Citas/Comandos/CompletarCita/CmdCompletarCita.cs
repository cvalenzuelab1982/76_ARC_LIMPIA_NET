using DientesLimpios.Aplicacion.Utilidades.Mediador;

namespace DientesLimpios.Aplicacion.CasosDeUso.Citas.Comandos.CompletarCita
{
    public class CmdCompletarCita : IRequest
    {
        public required Guid Id { get; set; }
    }
}
