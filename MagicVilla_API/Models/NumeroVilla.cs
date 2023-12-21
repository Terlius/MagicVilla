using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVilla_API.Models
{
    public class NumeroVilla
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]

        public int Numero { get; set; }
        [Required]
        public int IdVilla { get; set; }
        [ForeignKey("IdVilla")]
        public Villa Villa { get; set; }
        public string Detalles { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
