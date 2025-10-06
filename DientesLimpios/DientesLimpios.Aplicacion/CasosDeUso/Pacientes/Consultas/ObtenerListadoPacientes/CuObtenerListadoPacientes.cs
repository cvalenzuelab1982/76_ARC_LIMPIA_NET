using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Utilidades.Comunes;
using DientesLimpios.Aplicacion.Utilidades.Mediador;

namespace DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Consultas.ObtenerListadoPacientes
{
    public class CuObtenerListadoPacientes : IRequestHandler<ConsultaObtenerListadoPacientes, PaginadoDTO<PacienteListadoDTO>>
    {
        private readonly IRepositoriosPacientes _repositorios;


        public CuObtenerListadoPacientes(IRepositoriosPacientes repositorios)
        {
            _repositorios = repositorios;
        }


        public async Task<PaginadoDTO<PacienteListadoDTO>> Handle(ConsultaObtenerListadoPacientes request)
        {
            var pacientes = await _repositorios.ObtenerFiltrado(request);
            var totalPacientes = await _repositorios.ObtenerCantidadTotalRegistros();
            var pacienteDTO = pacientes.Select(paciente => paciente.ADto()).ToList();

            var paginadoDTO = new PaginadoDTO<PacienteListadoDTO>
            {
                Elementos = pacienteDTO,
                Total = totalPacientes
            };

            return paginadoDTO;
        }
    }
}
