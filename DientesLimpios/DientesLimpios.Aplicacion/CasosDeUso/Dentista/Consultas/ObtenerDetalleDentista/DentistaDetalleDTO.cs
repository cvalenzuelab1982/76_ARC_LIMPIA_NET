namespace DientesLimpios.Aplicacion.CasosDeUso.Dentista.Consultas.ObtenerDetalleDentista
{
    public class DentistaDetalleDTO
    {
        public Guid Id { get; set; }
        public required string Nombre { get; set; }
        public required string Email { get; set; }
    }
}
