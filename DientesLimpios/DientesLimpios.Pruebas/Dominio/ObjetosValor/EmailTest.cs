using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosValor;

namespace DientesLimpios.Pruebas.Dominio.ObjetosValor
{
    [TestClass]
    public class EmailTest
    {
        [TestMethod]
        [ExpectedException(typeof(ExcepcionDeReglaDeNegocio))]
        public void Constructor_EmailNulo_LanzaExcepcion()
        {
            new Email(null!);
        }

        [TestMethod]
        [ExpectedException(typeof(ExcepcionDeReglaDeNegocio))]
        public void Constructor_EmailSinArroba_LanzaExcepcion()
        {
            new Email("carlos.pe");
        }

        [TestMethod]
        public void Constructor_EmailValido_NoLanzaExcepcion()
        {
            new Email("carlos@correo.pe");
        }
    }
}
