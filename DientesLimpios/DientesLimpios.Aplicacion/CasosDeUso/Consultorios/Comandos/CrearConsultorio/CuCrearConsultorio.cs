using DientesLimpios.Aplicacion.Contratos.Persitencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using DientesLimpios.Dominio.Entidades;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio
{
    public class CuCrearConsultorio : IRequestHandler<CmdCrearConsultorio, Guid>
    {
        private readonly IRepositorioConsultorios _Repositorio;
        private readonly IUnidadDeTrabajo _UnidadDeTrabajo;

        public CuCrearConsultorio(IRepositorioConsultorios repositorio, IUnidadDeTrabajo unidadDeTrabajo)
        {
            _Repositorio = repositorio;
            _UnidadDeTrabajo = unidadDeTrabajo;
        }

        public async Task<Guid> Handle(CmdCrearConsultorio comando)
        {
            var consultorio = new Consultorio(comando.Nombre);

            try
            {
                var respuesta = await _Repositorio.Agregar(consultorio);
                await _UnidadDeTrabajo.Persistir();
                return respuesta!.Id;
            }
            catch (Exception)
            {
                await _UnidadDeTrabajo.Reversar();
                throw;
            }
        }
    }
}
