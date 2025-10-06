using DientesLimpios.Aplicacion.Contratos.Persitencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;

namespace DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.BorrarPaciente
{
    public class CuBorrarPaciente : IRequestHandler<CmdBorrarPaciente>
    {
        private readonly IRepositoriosPacientes _repositorio;
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public CuBorrarPaciente(IRepositoriosPacientes repositorio, IUnidadDeTrabajo unidadDeTrabajo)
        {
            _repositorio = repositorio;
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public async Task Handle(CmdBorrarPaciente request)
        {
            var paciente = await _repositorio.ObtenerPorId(request.Id);

            if (paciente is null)
            {
                throw new ExcepcionNoEncontrado();
            }

            try
            {
                await _repositorio.Borrar(paciente);
                await _unidadDeTrabajo.Persistir();
            }
            catch (Exception)
            {
                await _unidadDeTrabajo.Reversar();
                throw;
            }
        }
    }
}
