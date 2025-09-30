using System.ComponentModel.DataAnnotations;

namespace DientesLimpios.API.Dtos.Consultorios
{
    public class CrearConsultorioDTO
    {
        [Required]
        [StringLength(150)]
        public required string Nombre { get; set; } 
    }
}
