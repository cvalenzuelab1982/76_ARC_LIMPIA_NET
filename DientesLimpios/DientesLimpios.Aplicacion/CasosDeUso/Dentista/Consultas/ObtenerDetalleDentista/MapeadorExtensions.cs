using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Consultas.ObtenerDetallePaciente;
using DientesLimpios.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Dentista.Consultas.ObtenerDetalleDentista
{
    public static class MapeadorExtensions
    {
        public static DentistaDetalleDTO ADto(this DientesLimpios.Dominio.Entidades.Dentista paciente)
        {
            var dto = new DentistaDetalleDTO
            {
                Id = paciente.Id,
                Nombre = paciente.Nombre,
                Email = paciente.Email.Valor
            };

            return dto;
        }
    }
}
