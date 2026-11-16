using System.ComponentModel.DataAnnotations;

namespace HostingServidoresVideojuegos.Models.DTOs;

public class CrearUsuarioDto
{
    [Required]
    [StringLength(100)]
    public string NombreUsuario { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(150)]
    public string Correo { get; set; } = string.Empty;

    [Required]
    [MinLength(8)]
    public string Password { get; set; } = string.Empty;
}