using DientesLimpios.Aplicacion.Contratos.Notificaciones;
using DientesLimpios.Aplicacion.Contratos.Persitencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.ObjetosValor;

namespace DientesLimpios.Aplicacion.CasosDeUso.Citas.Comandos.CrearCita
{
    public class CuCrearCita : IRequestHandler<CmdCrearCita, Guid>
    {
        private readonly IRepositorioCitas _repositorio;
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IServicioNotificaciones _servicioNotificaciones;

        public CuCrearCita(IRepositorioCitas repositorio, IUnidadDeTrabajo unidadDeTrabajo, IServicioNotificaciones servicioNotificaciones)
        {
            _repositorio = repositorio;
            _unidadDeTrabajo = unidadDeTrabajo;
            _servicioNotificaciones = servicioNotificaciones;
        }

        public async Task<Guid> Handle(CmdCrearCita request)
        {
            var citaSeSolapa = await _repositorio.ExisteCitaSolapada(request.DentistaId, request.FechaInicio, request.FechaFin);

            if (citaSeSolapa)
            {
                throw new ExcepcionDeValidacion("El dentista ya tiene una cita en ese horario");
            }

            var intervaloDeTiempo = new IntervaloDeTiempo(request.FechaInicio, request.FechaFin);
            var cita = new Cita(request.PacienteId, request.DentistaId, request.ConsultorioId, intervaloDeTiempo);

            Guid? id = null;    

            try
            {
                var respuesta = await _repositorio.Agregar(cita);
                await _unidadDeTrabajo.Persistir();
                id = respuesta.Id;
            }
            catch (Exception)
            {
                await _unidadDeTrabajo.Reversar();
                throw;
            }

            var citaDB = await _repositorio.ObtenerPorId(id.Value);
            var notificacionDTO = citaDB!.ADto();
            await _servicioNotificaciones.EnviarConfirmacionCita(notificacionDTO);
            return id.Value;
        }

    }
}
