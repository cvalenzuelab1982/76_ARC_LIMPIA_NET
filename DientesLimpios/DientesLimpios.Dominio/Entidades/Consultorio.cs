using DientesLimpios.Dominio.Comunes;
using DientesLimpios.Dominio.Excepciones;

namespace DientesLimpios.Dominio.Entidades
{
    public class Consultorio : EntidadAuditable
    {
        public Guid Id { get; private set; }
        public string Nombre { get; private set; } = null!;

        public Consultorio(string nombre)
        {
            AplicarReglaDeNegocioNombre(nombre);

            Id = Guid.CreateVersion7();
            Nombre = nombre;
        }

        public void ActualizarNombre(string nombre)
        {
            AplicarReglaDeNegocioNombre(nombre);

            Nombre = nombre;
        }

        private void AplicarReglaDeNegocioNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ExcepcionDeReglaDeNegocio($"El {nameof(nombre)} es obligatorio");
            }
        }
    }
}
