
namespace HostingServidoresVideojuegos.Models;

public class Usuario
{
    public int IdUsuario { get; set; }

    public string NombreUsuario { get; set; } = string.Empty;

    public string Correo { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public bool Estado { get; set; } = true;

    public DateTime FechaCreacion { get; set; }
}
