using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parcial.Models
{
    public class pedidos_proceso
    {
        [Key]
        public int id_pedido { get; set; }
        public string? cliente { get; set; } 
        public string? mesa_numero { get; set; }
        public string? mesero { get; set; }
        public string? estado { get; set; }
        public string? fecha { get; set; }
    }
}
