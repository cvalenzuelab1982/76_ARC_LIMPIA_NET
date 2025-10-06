using DientesLimpios.Aplicacion.Contratos.Persitencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.ObjetosValor;

namespace DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.CrearPaciente
{
    public class CuCrearPaciente : IRequestHandler<CmdCrearPaciente, Guid>
    {
        private readonly IRepositoriosPacientes _repositorio;
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;


        public CuCrearPaciente(IRepositoriosPacientes repositorio, IUnidadDeTrabajo unidadDeTrabajo)
        {
            _repositorio = repositorio;
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public async Task<Guid> Handle(CmdCrearPaciente request)
        {
            var email = new Email(request.Email);
            var paciente = new Paciente(request.Nombre, email);

            try
            {
                var respuesta = await _repositorio.Agregar(paciente);
                await _unidadDeTrabajo.Persistir();
                return respuesta.Id;
            }
            catch (Exception)
            {
                await _unidadDeTrabajo.Reversar();
                throw;
            }
        }
    }
}
