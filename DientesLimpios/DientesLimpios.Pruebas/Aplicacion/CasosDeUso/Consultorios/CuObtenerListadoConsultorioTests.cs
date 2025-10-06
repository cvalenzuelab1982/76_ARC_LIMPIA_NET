﻿using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Consultas.ObtenerListadoConsultorios;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Dominio.Entidades;
using NSubstitute;

namespace DientesLimpios.Pruebas.Aplicacion.CasosDeUso.Consultorios
{
    [TestClass]
    public class CuObtenerListadoConsultorioTests
    {
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de agregar el modificador "required" o declararlo como un valor que acepta valores NULL.
        private IRepositorioConsultorios _repositorio;
        private CuObtenerListadoConsultorios _cu;
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de agregar el modificador "required" o declararlo como un valor que acepta valores NULL.

        [TestInitialize]
        public void Setup()
        {
            _repositorio = Substitute.For<IRepositorioConsultorios>();
            _cu = new CuObtenerListadoConsultorios(_repositorio);
        }

        [TestMethod]
        public async Task Handle_CuandoHayConsultorios_RetornarListaDeConsultorioListadoDTO()
        {
            var consultorio = new List<Consultorio>
            {
                new Consultorio("Consultorio A"),
                new Consultorio("Consultorio B")
            };

            _repositorio.ObtenerTodos().Returns(consultorio);

            var esperado = consultorio.Select(c => new ConsultorioListadoDTO { Id = c.Id, Nombre = c.Nombre }).ToList();

            var resultado = await _cu.Handle(new ConsultaObtenerListadoConsultorios());

            Assert.AreEqual(esperado.Count, resultado.Count);

            for (int i = 0; i < esperado.Count; i++)
            {
                Assert.AreEqual(esperado[i].Id, resultado[i].Id);
                Assert.AreEqual(esperado[i].Nombre, resultado[i].Nombre);
            }
        }

        [TestMethod]
        public async Task Handle_CuandoNoHayConsultorios_RetornarListaVacia()
        {
            _repositorio.ObtenerTodos().Returns(new List<Consultorio>());
            var resultado = await _cu.Handle(new ConsultaObtenerListadoConsultorios());
            Assert.IsNotNull(resultado);
            Assert.AreEqual(0, resultado.Count);

        }
    }
}
