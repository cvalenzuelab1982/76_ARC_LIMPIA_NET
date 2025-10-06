using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.CrearPaciente;
using DientesLimpios.Aplicacion.Contratos.Persitencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.ObjetosValor;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Pruebas.Aplicacion.CasosDeUso.Pacientes
{
    [TestClass]
    public class CuCrearPacienteTests
    {
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de agregar el modificador "required" o declararlo como un valor que acepta valores NULL.
        private IRepositoriosPacientes _repositorio;
        private IUnidadDeTrabajo _unidadDeTrabajo;
        private CuCrearPaciente _cu;
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de agregar el modificador "required" o declararlo como un valor que acepta valores NULL.

        [TestInitialize]
        public void Setup()
        {
            _repositorio = Substitute.For<IRepositoriosPacientes>();
            _unidadDeTrabajo = Substitute.For<IUnidadDeTrabajo>();
            _cu = new CuCrearPaciente(_repositorio, _unidadDeTrabajo);
        }

        [TestMethod]
        public async Task Handle_CuandoDatosValidos_CrearPacienteYPersisteYRetornaId()
        {
            var cmd = new CmdCrearPaciente { Nombre = "Felipe", Email = "felipe@correo.pe" };
            var pacienteCreado = new Paciente(cmd.Nombre, new Email(cmd.Email));
            var id = pacienteCreado.Id;

            _repositorio.Agregar(Arg.Any<Paciente>()).Returns(pacienteCreado);

            var idResultado = await _cu.Handle(cmd);

            Assert.AreEqual(id, idResultado);
            await _repositorio.Received(1).Agregar(Arg.Any<Paciente>());
            await _unidadDeTrabajo.Received(1).Persistir();
        }

        [TestMethod]
        public async Task Handle_CuandoOcurreExcepcion_ReversarYLanzaExcepcion()
        {
            var cmd = new CmdCrearPaciente { Nombre = "Felipe", Email = "felipe@correo.pe" };
            _repositorio.Agregar(Arg.Any<Paciente>()).Throws(new InvalidOperationException("Error al insertar"));

            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => _cu.Handle(cmd));
            await _unidadDeTrabajo.Received(1).Reversar();
            await _unidadDeTrabajo.DidNotReceive().Persistir();

        }

    }
}
