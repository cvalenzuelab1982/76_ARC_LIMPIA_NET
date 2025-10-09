using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Utilidades.Mediador;

namespace DientesLimpios.Aplicacion.CasosDeUso.Citas.Consultas.ObtenerListadoCitas
{
    public class CuObtenerListadoCitas : IRequestHandler<ConsultaObtenerListadoCitas, List<CitaListadoDTO>>
    {
        private readonly IRepositorioCitas _repositorio;

        public CuObtenerListadoCitas(IRepositorioCitas repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<CitaListadoDTO>> Handle(ConsultaObtenerListadoCitas request)
        {
            var citas = await _repositorio.ObtenerFiltrado(request);
            var citasDTO = citas.Select(cita => cita.ADto()).ToList();
            return citasDTO;
        }
    }
}
