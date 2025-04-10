using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Parcial.Models
{
    public class cocineros
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // AUTOINCREMENTAL
        public int id_cocinero { get; set; }

        [StringLength(255)]
        public string? nombre { get; set; }

        public int platos_preparados { get; set; }

        [StringLength(255)]
        public string? comentario { get; set; }

    }
}
