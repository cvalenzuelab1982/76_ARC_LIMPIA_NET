using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio;
using DientesLimpios.Aplicacion.Contratos.Persitencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Dominio.Entidades;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Pruebas.Aplicacion.CasosDeUso.Consultorios
{
    [TestClass]
    public class CuCrearConsultorioTests
    {
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de agregar el modificador "required" o declararlo como un valor que acepta valores NULL.
        private IRepositorioConsultorios _Repositorio;
        private IValidator<CmdCrearConsultorio> _Validador;
        private IUnidadDeTrabajo _UnidadDeTrabajo;
        private CuCrearConsultorio _CasoDeUso;
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de agregar el modificador "required" o declararlo como un valor que acepta valores NULL.

        [TestInitialize]
        public void Setup()
        {
            _Repositorio = Substitute.For<IRepositorioConsultorios>();
            _Validador = Substitute.For<IValidator<CmdCrearConsultorio>>();
            _UnidadDeTrabajo = Substitute.For<IUnidadDeTrabajo>();
            _CasoDeUso = new CuCrearConsultorio(_Repositorio, _UnidadDeTrabajo, _Validador);
        }

        [TestMethod]
        public async Task Handle_ComandoValido_ObtenerIdConsultorio()
        {
            var comando = new CmdCrearConsultorio { Nombre = "Consultorio A" };

            _Validador.ValidateAsync(comando).Returns(new ValidationResult());

            var consultorioCreado = new Consultorio("Consultorio A");
            _Repositorio.Agregar(Arg.Any<Consultorio>()).Returns(consultorioCreado);

            var resultado = await _CasoDeUso.Handle(comando);

            await _Validador.Received(1).ValidateAsync(comando);
            await _Repositorio.Received(1).Agregar(Arg.Any<Consultorio>());
            await _UnidadDeTrabajo.Received(1).Persistir();
            Assert.AreNotEqual(Guid.Empty, resultado);
        }

        [TestMethod]
        public async Task Handle_ComandoNoValido_LanzaExcepcion()
        {
            var comando = new CmdCrearConsultorio { Nombre = "" };

            var resultadoInvalido = new ValidationResult(new[]
            {
                new ValidationFailure("Nombre", "El nombre es obligatorio")
            });

            _Validador.ValidateAsync(comando).Returns(resultadoInvalido);

            await Assert.ThrowsExceptionAsync<ExcepcionDeValidacion>(async () =>
            {
                await _CasoDeUso.Handle(comando);
            });

            await _Repositorio.DidNotReceive().Agregar(Arg.Any<Consultorio>());
        }

        [TestMethod]
        public async Task Handle_CuandoHayError_HacemosRollBack()
        {
            var comando = new CmdCrearConsultorio { Nombre = "Consultario A" };
            _Repositorio.Agregar(Arg.Any<Consultorio>()).Throws<Exception>();
            _Validador.ValidateAsync(comando).Returns(new ValidationResult());

            await Assert.ThrowsExceptionAsync<Exception>(async () =>
            {
                var resultado = await _CasoDeUso.Handle(comando);
            });

            await _UnidadDeTrabajo.Received(1).Reversar();
        }
    }
}
