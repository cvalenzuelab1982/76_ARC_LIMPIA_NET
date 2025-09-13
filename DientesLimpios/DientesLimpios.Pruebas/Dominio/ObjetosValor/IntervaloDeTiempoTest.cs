using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Pruebas.Dominio.ObjetosValor
{
    [TestClass]
    public class IntervaloDeTiempoTest
    {
        [TestMethod]
        [ExpectedException(typeof(ExcepcionDeReglaDeNegocio))]
        public void Constructor_FechaInicioPosterior_A_FechaFin_LanzaExcepcion()
        {
            new IntervaloDeTiempo(DateTime.UtcNow, DateTime.UtcNow.AddDays(-1));
        }

        [TestMethod]
        public void Constructor_ParametrosCorrectos_NoLanzaExcepcion()
        {
            new IntervaloDeTiempo(DateTime.UtcNow, DateTime.UtcNow.AddMinutes(30));
        }
    }
}
