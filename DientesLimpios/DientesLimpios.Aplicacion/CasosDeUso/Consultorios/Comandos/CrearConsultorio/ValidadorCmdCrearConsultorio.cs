using FluentValidation;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio
{
    public class ValidadorCmdCrearConsultorio : AbstractValidator<CmdCrearConsultorio>
    {
        public ValidadorCmdCrearConsultorio()
        {
            RuleFor(p => p.Nombre).NotEmpty().WithMessage("Em campo {PropertyName} es requerido")
            .MaximumLength(150).WithMessage("La longitud del campo {PropertyName} debe ser menor o igual a {MaxLength}");
        }
    }
}
