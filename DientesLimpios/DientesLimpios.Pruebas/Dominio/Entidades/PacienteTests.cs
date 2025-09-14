using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosValor;

namespace DientesLimpios.Pruebas.Dominio.Entidades
{
    [TestClass]
    public class PacienteTests
    {
        [TestMethod]
        [ExpectedException(typeof(ExcepcionDeReglaDeNegocio))]
        public void Constructor_NombreNulo_LanzaExcepcion()
        {
            var email = new Email("carlos@correo.pe");
            new Paciente(null!, email);
        }

        [TestMethod]
        [ExpectedException(typeof(ExcepcionDeReglaDeNegocio))]
        public void Constructor_EmailNulo_LanzaExcepcion()
        {
            Email email = null!;
            new Paciente("Carlos", email);
        }
    }
}
