using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Models.DTO
{
    public class VillaDTO
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(30)]
        public string Nombre { get; set; }
        public string Detalles { get; set; } = "Villa";
        public double Tarifa { get; set; }
        public string ImagenURL { get; set; }
        public string Amenidades { get; set; }
        public int Ocupantes { get; set; }
        public int MetrosCuadrados { get; set; }

    }
}
