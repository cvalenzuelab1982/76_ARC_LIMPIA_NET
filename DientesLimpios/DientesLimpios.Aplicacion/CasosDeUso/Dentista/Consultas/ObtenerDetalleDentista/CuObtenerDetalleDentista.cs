using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Consultas.ObtenerDetallePaciente;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Dentista.Consultas.ObtenerDetalleDentista
{
    public class CuObtenerDetalleDentista : IRequestHandler<ConsultaObtenerDetalleDentista, DentistaDetalleDTO>
    {
        private readonly IRepositorioDentista _repositorios;

        public CuObtenerDetalleDentista(IRepositorioDentista repositorios)
        {
            _repositorios = repositorios;
        }

        public async Task<DentistaDetalleDTO> Handle(ConsultaObtenerDetalleDentista request)
        {
            var dentista = await _repositorios.ObtenerPorId(request.Id);
            if (dentista is null)
            {
                throw new ExcepcionNoEncontrado();
            }

            return dentista.ADto();
        }
    }
}
