using FluentValidation;

namespace DientesLimpios.Aplicacion.CasosDeUso.Citas.Comandos.CrearCita
{
    public class ValidadorCmdCrearCita : AbstractValidator<CmdCrearCita>
    {
        public ValidadorCmdCrearCita()
        {
            RuleFor(x => x.FechaInicio)
                .GreaterThan(x => x.FechaFin).WithMessage("La fecha fin debe ser posterior a la fecha de inicio")
                .GreaterThan(DateTime.UtcNow).WithMessage("La fecha inicio no puede ser en el pasado");
        }
    }
}
