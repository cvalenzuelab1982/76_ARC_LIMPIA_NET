namespace DientesLimpios.Aplicacion.CasosDeUso.Dentista.Consultas.ObtenerListadoPacientes
{
    public class FiltroDentistaDTO
    {
        public int Pagina { get; set; } = 1;
        public int RegistroPorPagina { get; set; } = 10;
        public string? Nombre { get; set; }
        public string? Email { get; set; }
    }
}
