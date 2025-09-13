using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.Excepciones;

namespace DientesLimpios.Pruebas.Dominio.Entidades
{
    [TestClass]
    public class ConsultorioTest
    {
        [TestMethod]
        [ExpectedException(typeof(ExcepcionDeReglaDeNegocio))]
        public void Constructor_NombreNulo_LanzaExcepcion()
        {
            new Consultorio(null!);
        }
    }
}
