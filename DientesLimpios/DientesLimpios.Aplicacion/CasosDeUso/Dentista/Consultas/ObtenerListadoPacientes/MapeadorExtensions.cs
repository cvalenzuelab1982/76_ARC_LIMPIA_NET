using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Consultas.ObtenerListadoPacientes;

namespace DientesLimpios.Aplicacion.CasosDeUso.Dentista.Consultas.ObtenerListadoPacientes
{
    public static class MapeadorExtensions
    {
        public static DentistaListadoDTO ADto(this DientesLimpios.Dominio.Entidades.Dentista paciente)
        {
            var dto = new DentistaListadoDTO
            {
                Id = paciente.Id,
                Nombre = paciente.Nombre,
                Email = paciente.Email.Valor
            };

            return dto;
        }
    }
}
