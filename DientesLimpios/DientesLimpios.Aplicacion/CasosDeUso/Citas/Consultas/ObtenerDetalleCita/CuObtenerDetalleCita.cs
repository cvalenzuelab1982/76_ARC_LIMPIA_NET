using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;

namespace DientesLimpios.Aplicacion.CasosDeUso.Citas.Consultas.ObtenerDetalleCita
{
    public class CuObtenerDetalleCita : IRequestHandler<ConsultaObtenerDetalleCita, CitaDetalleDTO>
    {
        private readonly IRepositorioCitas _repositorio;

        public CuObtenerDetalleCita(IRepositorioCitas repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<CitaDetalleDTO> Handle(ConsultaObtenerDetalleCita request)
        {
            var cita = await _repositorio.ObtenerPorId(request.Id);

            if (cita is null)
            {
                throw new ExcepcionNoEncontrado();
            }

            return cita.ADto();
        }
    }
}
