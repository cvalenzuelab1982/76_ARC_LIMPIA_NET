﻿using FluentValidation;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.ActualizarConsultorio
{
    public class ValidadorCmdActualizarConsultorio : AbstractValidator<CmdActualizarConsultorio>
    {
        public ValidadorCmdActualizarConsultorio()
        {
            RuleFor(p => p.Nombre)
                .NotEmpty().WithMessage("El campo {PropertyName} es requerido")
                .MaximumLength(150).WithMessage("La longitud del campo {PropertyName} debe ser menor o igual a {MaxLength}");
        }
    }
}
