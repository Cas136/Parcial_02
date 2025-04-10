using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parcial.Models
{
    [Table("platos")]
    public class platos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // AUTOINCREMENTAL
        public int id_plato { get; set; }

        [StringLength(255)]
        public string? nombre { get; set; }

        [StringLength(255)]
        public string? imagen { get; set; }

        public int cantidad_vendida { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal ingresos_generados { get; set; }
    }
}