using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Models.DTO
{
    public class NumeroVillaUpdateDTO
    {
        [Required]

        public int Numero { get; set; }
        [Required]
        public int IdVilla { get; set; }

        public string Detalles { get; set; }
    }
}
