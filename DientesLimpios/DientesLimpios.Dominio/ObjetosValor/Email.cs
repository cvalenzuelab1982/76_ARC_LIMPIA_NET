using DientesLimpios.Dominio.Excepciones;

namespace DientesLimpios.Dominio.ObjetosValor
{
    public record Email
    {
        public string Valor { get; } = null!;

        private Email()
        {
            
        }

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
