namespace HostingServidoresVideojuegos.Models.DTOs;

public class UsuarioRespuestaDto
{
    public int IdUsuario { get; set; }

    public string NombreUsuario { get; set; } = string.Empty;

    public string Correo { get; set; } = string.Empty;

    public bool Estado { get; set; }

    public DateTime FechaCreacion { get; set; }
}