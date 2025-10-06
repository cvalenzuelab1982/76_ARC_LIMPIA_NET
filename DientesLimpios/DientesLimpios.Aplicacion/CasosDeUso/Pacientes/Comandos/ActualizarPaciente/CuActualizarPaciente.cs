using DientesLimpios.Aplicacion.Contratos.Persitencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using DientesLimpios.Dominio.ObjetosValor;

namespace DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.ActualizarPaciente
{
    public class CuActualizarPaciente : IRequestHandler<CmdActualizarPaciente>
    {
        private readonly IRepositoriosPacientes _repositorio;
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public CuActualizarPaciente(IRepositoriosPacientes repositorio, IUnidadDeTrabajo unidadDeTrabajo)
        {
            _repositorio = repositorio;
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public async Task Handle(CmdActualizarPaciente request)
        {
            var paciente = await _repositorio.ObtenerPorId(request.Id);

            if (paciente is null)
            {
                throw new ExcepcionNoEncontrado();
            }

            paciente.ActualizarNombre(request.Nombre);
            var email = new Email(request.Email);
            paciente.ActualizarEmail(email);

            try
            {
                await _repositorio.Actualizar(paciente);
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
