using System.ComponentModel.DataAnnotations;
namespace Parcial.Models
{
    public class mesas
    {
        [Key]
        public int id_mesa { get; set; }
        public string? ubicacion { get; set; }
        public int capacidad { get; set; }
        public string? estado { get; set; }
    }
}
