using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Dentista.Comandos.BorrarDentista
{
    public class CmdBorrarDentista : IRequest
    {
        public required Guid Id { get; set; }
    }
}
