using DientesLimpios.Dominio.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Dominio.ObjetosValor
{
    public record Email
    {
        public string Valor { get; } = null!;

        public Email(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ExcepcionDeReglaDeNegocio($"El {nameof(email)} es obligatorio");
            }

            if (!email.Contains("@"))
            {
                throw new ExcepcionDeReglaDeNegocio($"El {nameof(email)} no es valido");
            }

            Valor = email;  
        }
    }
}
