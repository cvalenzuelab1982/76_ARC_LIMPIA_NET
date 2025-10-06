using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.ActualizarConsultorio;
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
    public class CuActualizarConsultorioTests
    {
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de agregar el modificador "required" o declararlo como un valor que acepta valores NULL.
        private IRepositorioConsultorios _repositorio;
        private IUnidadDeTrabajo _unidadDeTrabajo;
        private CuActualizarConsultorio _cu;
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de agregar el modificador "required" o declararlo como un valor que acepta valores NULL.

        [TestInitialize]
        public void Setup()
        {
            _repositorio = Substitute.For<IRepositorioConsultorios>();
            _unidadDeTrabajo = Substitute.For<IUnidadDeTrabajo>();
            _cu = new CuActualizarConsultorio(_repositorio, _unidadDeTrabajo);
        }

        [TestMethod]
        public async Task Handle_CuandoConsultorioExiste_ActualizarNombreYPersiste()
        {
            var consultorio = new Consultorio("Consultorio A");
            var id = consultorio.Id;
            var cmd = new CmdActualizarConsultorio { Id = id, Nombre = "Nuevo nombre" };

            _repositorio.ObtenerPorId(id).Returns(consultorio);

            await _cu.Handle(cmd);
            await _repositorio.Received(1).Actualizar(consultorio);
            await _unidadDeTrabajo.Received(1).Persistir();
        }

        [TestMethod]
        [ExpectedException(typeof(ExcepcionNoEncontrado))]
        public async Task Handle_CuandoConsultorioNoExiste_LanzaExcepcionNoEncontrado()
        {
            var cmd = new CmdActualizarConsultorio { Id = Guid.NewGuid(), Nombre = "Nombre" };
            _repositorio.ObtenerPorId(cmd.Id).ReturnsNull();

            await _cu.Handle(cmd);
        }

        [TestMethod]
        public async Task Handle_CuandoOcurreExcepcionActualizar_LlamarAReversarYLanzaExcepcion()
        {
            var consultorio = new Consultorio("Consultorio A");
            var id = consultorio.Id;
            var cmd = new CmdActualizarConsultorio { Id = id, Nombre = "Consultorio B" };

            _repositorio.ObtenerPorId(id).Returns(consultorio);
            _repositorio.Actualizar(consultorio).Throws(new InvalidOperationException("Error al actualizar"));

            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => _cu.Handle(cmd));
            await _unidadDeTrabajo.Received(1).Reversar();
        }
    }
}
