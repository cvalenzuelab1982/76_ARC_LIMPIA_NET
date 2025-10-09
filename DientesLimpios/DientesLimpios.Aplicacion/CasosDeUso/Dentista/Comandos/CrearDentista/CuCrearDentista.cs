using DientesLimpios.Aplicacion.Contratos.Persitencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.ObjetosValor;

namespace DientesLimpios.Aplicacion.CasosDeUso.Dentista.Comandos.CrearDentista
{
    public class CuCrearDentista : IRequestHandler<CmdCrearDentista, Guid>
    {
        private readonly IRepositorioDentista _repositorio;
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;


        public CuCrearDentista(IRepositorioDentista repositorio, IUnidadDeTrabajo unidadDeTrabajo)
        {
            _repositorio = repositorio;
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public async Task<Guid> Handle(CmdCrearDentista request)
        {
            var email = new Email(request.Email);
            var dentista = new DientesLimpios.Dominio.Entidades.Dentista(request.Nombre, email);

            try
            {
                var respuesta = await _repositorio.Agregar(dentista);
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
