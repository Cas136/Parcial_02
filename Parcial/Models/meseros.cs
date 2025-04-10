using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parcial.Models
{
    public class meseros
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_mesero { get; set; }

        [Required]
        [StringLength(100)]
        public string nombre { get; set; }

        public int? numero_pedidos { get; set; } = 0;  // Nullable con valor por defecto

        [Column(TypeName = "decimal(18,2)")]
        public decimal? total_ventas { get; set; } = 0.00m;  // Nullable con valor por defecto
    }
}
