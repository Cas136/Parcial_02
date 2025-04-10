namespace Parcial.Models;
// Models/Usuario.cs
using System.ComponentModel.DataAnnotations;

public class Usuario
{
    [Key]
    public int id_usuario { get; set; }

    [Required]
    [EmailAddress]
    [Display(Name = "Gmail")]
    public string gmail { get; set; }

    [Required]
    [Display(Name = "Contraseña")]
    public string contrasena { get; set; } // Debería estar hasheada en producción
}