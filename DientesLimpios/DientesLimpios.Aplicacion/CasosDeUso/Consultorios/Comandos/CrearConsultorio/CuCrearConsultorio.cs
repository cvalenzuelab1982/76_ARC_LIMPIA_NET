using DientesLimpios.Aplicacion.Contratos.Persitencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Dominio.Entidades;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio
{
    public class CuCrearConsultorio
    {
        private readonly IRepositorioConsultorios _Repositorio;
        private readonly IUnidadDeTrabajo _UnidadDeTrabajo;
        private readonly IValidator<CmdCrearConsultorio> _Validador;

        public CuCrearConsultorio(IRepositorioConsultorios repositorio, IUnidadDeTrabajo unidadDeTrabajo, IValidator<CmdCrearConsultorio> validador)
        {
            _Repositorio = repositorio;
            _UnidadDeTrabajo = unidadDeTrabajo;
            _Validador = validador;
        }

        public async Task<Guid> Handle(CmdCrearConsultorio comando)
        {
            var resultadoValidacion = await _Validador.ValidateAsync(comando);
            if (!resultadoValidacion.IsValid)
            {
                throw new ExcepcionDeValidacion(resultadoValidacion);
            }

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
