using DientesLimpios.Aplicacion.Contratos.Notificaciones;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Contratos.Repositorios.Modelos;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using DientesLimpios.Dominio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Citas.Comandos.EnviarRecordatorioCitas
{
    public class CuEnviarRecordatorioCitas : IRequestHandler<CmdEnviarRecordatorioCitas>
    {
        private readonly IRepositorioCitas _repositorio;    
        private readonly IServicioNotificaciones _servicioNotificaciones;

        public CuEnviarRecordatorioCitas(IRepositorioCitas repositorio, IServicioNotificaciones servicioNotificaciones)
        {
            _repositorio = repositorio;
            _servicioNotificaciones = servicioNotificaciones;
        }

        public async Task Handle(CmdEnviarRecordatorioCitas request)
        {
            var manana = DateTime.UtcNow.Date.AddDays(1);
            var fechaIncio = manana;
            var fechaFin = manana.AddDays(1);
            var filtro = new FiltroCitasDTO
            {
                FechaInicio = fechaIncio,
                FechaFin = fechaFin,
                EstadoCita = EstadoCita.Programada
            };

            var citas = await _repositorio.ObtenerFiltrado(filtro);
            foreach (var cita in citas)
            {
                var citaDTO = cita.ADto();
                await _servicioNotificaciones.EnviarRecordatorioCita(citaDTO);
            }
        }
    }
}
