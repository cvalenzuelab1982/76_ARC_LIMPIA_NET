using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Consultas.ObtenerDetalleConsultorio
{
    public class CuObtenerDetalleConsultorio : IRequestHandler<ConsultaObtenerDetalleConsultorio, ConsultorioDetalleDTO>
    {
        private readonly IRepositorioConsultorios _Repositorio;

        public CuObtenerDetalleConsultorio(IRepositorioConsultorios repositorio)
        {
            _Repositorio = repositorio;
        }

        public async Task<ConsultorioDetalleDTO> Handle(ConsultaObtenerDetalleConsultorio request)
        {
            var consultorio = await _Repositorio.ObtenerPorId(request.Id);

            if (consultorio is null)
            {
                throw new ExcepcionNoEncontrado();
            }

            return consultorio.ADto();
        }
    }
}
