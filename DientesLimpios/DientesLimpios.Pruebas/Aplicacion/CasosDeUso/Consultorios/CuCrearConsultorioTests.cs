using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio;
using DientesLimpios.Aplicacion.Contratos.Persitencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Dominio.Entidades;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace DientesLimpios.Pruebas.Aplicacion.CasosDeUso.Consultorios
{
    [TestClass]
    public class CuCrearConsultorioTests
    {
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de agregar el modificador "required" o declararlo como un valor que acepta valores NULL.
        private IRepositorioConsultorios _Repositorio;
        private IUnidadDeTrabajo _UnidadDeTrabajo;
        private CuCrearConsultorio _CasoDeUso;
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de agregar el modificador "required" o declararlo como un valor que acepta valores NULL.

        [TestInitialize]
        public void Setup()
        {
            _Repositorio = Substitute.For<IRepositorioConsultorios>();
            _UnidadDeTrabajo = Substitute.For<IUnidadDeTrabajo>();
            _CasoDeUso = new CuCrearConsultorio(_Repositorio, _UnidadDeTrabajo);
        }

        [TestMethod]
        public async Task Handle_ComandoValido_ObtenerIdConsultorio()
        {
            var comando = new CmdCrearConsultorio { Nombre = "Consultorio A" };

            var consultorioCreado = new Consultorio("Consultorio A");
            _Repositorio.Agregar(Arg.Any<Consultorio>()).Returns(consultorioCreado);

            var resultado = await _CasoDeUso.Handle(comando);

            await _Repositorio.Received(1).Agregar(Arg.Any<Consultorio>());
            await _UnidadDeTrabajo.Received(1).Persistir();
            Assert.AreNotEqual(Guid.Empty, resultado);
        }

        [TestMethod]
        public async Task Handle_CuandoHayError_HacemosRollBack()
        {
            var comando = new CmdCrearConsultorio { Nombre = "Consultario A" };
            _Repositorio.Agregar(Arg.Any<Consultorio>()).Throws<Exception>();

            await Assert.ThrowsExceptionAsync<Exception>(async () =>
            {
                var resultado = await _CasoDeUso.Handle(comando);
            });

            await _UnidadDeTrabajo.Received(1).Reversar();
        }
    }
}
