using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Models.DTO
{
    public class VillaUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(30)]
        public string Nombre { get; set; }
        public string Detalles { get; set; } = "Villa";
        [Required]
        public double Tarifa { get; set; }
        [Required]
        public string ImagenURL { get; set; }
        public string Amenidades { get; set; }
        [Required]
        public int Ocupantes { get; set; }
        [Required]
        public int MetrosCuadrados { get; set; }

    }
}
