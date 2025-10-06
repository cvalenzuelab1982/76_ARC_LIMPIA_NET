using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Consultas.ObtenerDetallePaciente
{
    public class CuObtenerDetallePaciente : IRequestHandler<ConsultaObtenerDetallePaciente, PacienteDetalleDTO>
    {
        private readonly IRepositoriosPacientes _repositorios;

        public CuObtenerDetallePaciente(IRepositoriosPacientes repositorios)
        {
            _repositorios = repositorios;
        }

        public async Task<PacienteDetalleDTO> Handle(ConsultaObtenerDetallePaciente request)
        {
            var paciente = await _repositorios.ObtenerPorId(request.Id);
            if (paciente is null)
            {
                throw new ExcepcionNoEncontrado();
            }

            return paciente.ADto();
        }
    }
}
