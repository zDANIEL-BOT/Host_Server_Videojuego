using System.ComponentModel.DataAnnotations;

namespace HostingServidoresVideojuegos.Models.DTOs;

public class ActualizarUsuarioDto
{
    [Required]
    [StringLength(100)]
    public string NombreUsuario { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(150)]
    public string Correo { get; set; } = string.Empty;

    public bool Estado { get; set; }
}