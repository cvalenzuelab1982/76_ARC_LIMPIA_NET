using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Consultas.ObtenerListadoPacientes;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.ObjetosValor;
using NSubstitute;

namespace DientesLimpios.Pruebas.Aplicacion.CasosDeUso.Pacientes
{
    [TestClass]
    public class CuObtenerListadoPacientesTests
    {
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de agregar el modificador "required" o declararlo como un valor que acepta valores NULL.
        private IRepositoriosPacientes _repositorios;
        private CuObtenerListadoPacientes _cu;
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de agregar el modificador "required" o declararlo como un valor que acepta valores NULL.

        [TestInitialize]
        public void Setup()
        {
            _repositorios = Substitute.For<IRepositoriosPacientes>();
            _cu = new CuObtenerListadoPacientes(_repositorios);
        }

        [TestMethod]
        public async Task Handle_RetornarPacientesPaginadosCorrectamente()
        {
            var pagina = 1;
            var registrosPorPagina = 2;

            var filtroPacienteDTO = new FiltroPacienteDTO { Pagina = pagina, RegistroPorPagina = registrosPorPagina };

            var paciente1 = new Paciente("Felipe", new Email("felipe@correp.pe"));
            var paciente2 = new Paciente("Claudia", new Email("claudia@correp.pe"));

            IEnumerable<Paciente> pacientes = new List<Paciente> { paciente1, paciente2};

            _repositorios.ObtenerFiltrado(Arg.Any<FiltroPacienteDTO>()).Returns(Task.FromResult(pacientes));

            _repositorios.ObtenerCantidadTotalRegistros().Returns(Task.FromResult(10));

            var request = new ConsultaObtenerListadoPacientes
            {
                Pagina = pagina,
                RegistroPorPagina = registrosPorPagina
            };

            var resultado = await _cu.Handle(request);

            Assert.AreEqual(10, resultado.Total);
            Assert.AreEqual(2, resultado.Elementos.Count);
            Assert.AreEqual("Felipe", resultado.Elementos[0].Nombre);
            Assert.AreEqual("Claudia", resultado.Elementos[1].Nombre);
        }

        [TestMethod]
        public async Task Handle_CuandoNoHayPacientes_RetornaListaVaciaYTotalCero()
        {
            var pagina = 1;
            var registrosPorPagina = 5;

            var filtroPacienteDTO = new FiltroPacienteDTO { Pagina = pagina, RegistroPorPagina = registrosPorPagina };

            IEnumerable<Paciente> pacientes = new List<Paciente>();

            _repositorios.ObtenerFiltrado(Arg.Any<FiltroPacienteDTO>()).Returns(Task.FromResult(pacientes));

            _repositorios.ObtenerCantidadTotalRegistros().Returns(Task.FromResult(0));

            var request = new ConsultaObtenerListadoPacientes
            {
                Pagina = pagina,
                RegistroPorPagina = registrosPorPagina
            };

            var resultado = await _cu.Handle(request);

            Assert.AreEqual(0, resultado.Total);
            Assert.IsNotNull(resultado.Elementos);
            Assert.AreEqual(0, resultado.Elementos.Count);
        }
    }
}
