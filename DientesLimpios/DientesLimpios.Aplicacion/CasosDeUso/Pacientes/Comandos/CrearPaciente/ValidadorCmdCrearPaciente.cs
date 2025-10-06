using FluentValidation;

namespace DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.CrearPaciente
{
    public class ValidadorCmdCrearPaciente : AbstractValidator<CmdCrearPaciente>
    {
        public ValidadorCmdCrearPaciente()
        {
            RuleFor(p => p.Nombre)
                .NotEmpty().WithMessage("El campo {PropertyName} es requerido")
                .MaximumLength(250).WithMessage("La longitud del campo {PropertyName} debe ser menor o igual a {MaxLength}");

            RuleFor(p => p.Email)
               .NotEmpty().WithMessage("El campo {PropertyName} es requerido")
               .MaximumLength(254).WithMessage("La longitud del campo {PropertyName} debe ser menor o igual a {MaxLength}")
               .EmailAddress().WithMessage("El formato del email no es valido");
        }
    }
}
