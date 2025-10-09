using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Consultas.ObtenerListadoPacientes;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Utilidades.Comunes;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Dentista.Consultas.ObtenerListadoPacientes
{
    public class CuObtenerListadoDentistas : IRequestHandler<ConsultaObtenerListadoDentistas, PaginadoDTO<DentistaListadoDTO>>
    {
        private readonly IRepositorioDentista _repositorios;

        public CuObtenerListadoDentistas(IRepositorioDentista repositorios)
        {
            _repositorios = repositorios;
        }

        public async Task<PaginadoDTO<DentistaListadoDTO>> Handle(ConsultaObtenerListadoDentistas request)
        {
            var dentistas = await _repositorios.ObtenerFiltrado(request);
            var totalDentistas = await _repositorios.ObtenerCantidadTotalRegistros();
            var dentistasDTO = dentistas.Select(d => d.ADto()).ToList();

            var paginadoDTO = new PaginadoDTO<DentistaListadoDTO>
            {
                Elementos = dentistasDTO,
                Total = totalDentistas
            };

            return paginadoDTO;
        }
    }
}
