using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.BorrarConsultorio;
using DientesLimpios.Aplicacion.Contratos.Persitencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Dominio.Entidades;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;

namespace DientesLimpios.Pruebas.Aplicacion.CasosDeUso.Consultorios
{
    [TestClass]
    public class CuBorrarConsultorioTests
    {
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de agregar el modificador "required" o declararlo como un valor que acepta valores NULL.
        private IRepositorioConsultorios _Repositorio;
        private IUnidadDeTrabajo _UnidadDeTrabajo;
        private CuBorrarConsultorio _CasoDeUso;
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de agregar el modificador "required" o declararlo como un valor que acepta valores NULL.

        [TestInitialize]
        public void Setup()
        {
            _Repositorio = Substitute.For<IRepositorioConsultorios>();
            _UnidadDeTrabajo = Substitute.For<IUnidadDeTrabajo>();
            _CasoDeUso = new CuBorrarConsultorio(_Repositorio, _UnidadDeTrabajo);
        }

        [TestMethod]
        public async Task Handle_CuandoConsultorioExiste_BorraConsultorioYPersiste()
        {
            var id = Guid.NewGuid();
            var cmd = new CmdBorrarConsultorio { Id = id };
            var consultorio = new Consultorio("Consultorio A");

            _Repositorio.ObtenerPorId(id).Returns(consultorio);

            await _CasoDeUso.Handle(cmd);

            await _Repositorio.Received(1).Borrar(consultorio);
            await _UnidadDeTrabajo.Received(1).Persistir();
        }

        [TestMethod]
        [ExpectedException(typeof(ExcepcionNoEncontrado))]
        public async Task Handle_CuandoConsultorioNoExiste_LanzaExcepcionNoEncontrado()
        {
            var cmd =  new CmdBorrarConsultorio { Id= Guid.NewGuid() };
            _Repositorio.ObtenerPorId(cmd.Id).ReturnsNull();

            await _CasoDeUso.Handle(cmd);
        }

        [TestMethod]
        public async Task Handle_CuandoOcurreExcepcion_LlamaAReversarYLanzaExcepcion()
        {
            var id = Guid.NewGuid();
            var cmd = new CmdBorrarConsultorio { Id = id };
            var consultorio = new Consultorio("Consultorio A");

            _Repositorio.ObtenerPorId(id).Returns(consultorio);
            _Repositorio.Borrar(consultorio).Throws(new InvalidOperationException("Fallo al borrar"));

            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => _CasoDeUso.Handle(cmd));
            await _UnidadDeTrabajo.Received(1).Reversar();
        }
    }
}
