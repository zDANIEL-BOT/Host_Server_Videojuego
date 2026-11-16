using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HostingServidoresVideojuegos.Data;
using HostingServidoresVideojuegos.Models;
using HostingServidoresVideojuegos.Models.DTOs;

namespace HostingServidoresVideojuegos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsuariosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<UsuarioRespuestaDto>> Crear(
        CrearUsuarioDto dto)
    {
        var correo = dto.Correo.Trim().ToLowerInvariant();

        var correoExiste = await _context.Usuarios
            .AnyAsync(u => u.Correo == correo);

        if (correoExiste)
        {
            return Conflict(new
            {
                mensaje = "El correo ya está registrado."
            });
        }

        var usuario = new Usuario
        {
            NombreUsuario = dto.NombreUsuario,
            Correo = correo,

            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),

            Estado = true,
            FechaCreacion = DateTime.UtcNow
        };

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        var respuesta = new UsuarioRespuestaDto
        {
            IdUsuario = usuario.IdUsuario,
            NombreUsuario = usuario.NombreUsuario,
            Correo = usuario.Correo,
            Estado = usuario.Estado,
            FechaCreacion = usuario.FechaCreacion
        };

        return Created(
            $"/api/usuarios/{usuario.IdUsuario}",
            respuesta
        );
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UsuarioRespuestaDto>>> ObtenerTodos()
    {
        var usuarios = await _context.Usuarios
            .AsNoTracking()
            .Select(u => new UsuarioRespuestaDto
            {
                IdUsuario = u.IdUsuario,
                NombreUsuario = u.NombreUsuario,
                Correo = u.Correo,
                Estado = u.Estado,
                FechaCreacion = u.FechaCreacion
            })
            .ToListAsync();

        return Ok(usuarios);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UsuarioRespuestaDto>> ObtenerPorId(int id)
    {
        var usuario = await _context.Usuarios
            .AsNoTracking()
            .Where(u => u.IdUsuario == id)
            .Select(u => new UsuarioRespuestaDto
            {
                IdUsuario = u.IdUsuario,
                NombreUsuario = u.NombreUsuario,
                Correo = u.Correo,
                Estado = u.Estado,
                FechaCreacion = u.FechaCreacion
            })
            .FirstOrDefaultAsync();

        if (usuario == null)
        {
            return NotFound(new
            {
                mensaje = "No se encontró el usuario."
            });
        }

        return Ok(usuario);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Actualizar(int id, ActualizarUsuarioDto dto)
    {
        // Buscar el usuario por su ID
        var usuario = await _context.Usuarios.FindAsync(id);

        if (usuario == null)
        {
            return NotFound(new
            {
                mensaje = "No se encontró el usuario."
            });
        }

        // Normalizar el correo
        var correo = dto.Correo.Trim().ToLowerInvariant();

        // Comprobar que otro usuario no tenga ese correo
        var correoExiste = await _context.Usuarios
            .AnyAsync(u => u.Correo == correo && u.IdUsuario != id);

        if (correoExiste)
        {
            return Conflict(new
            {
                mensaje = "El correo ya está registrado por otro usuario."
            });
        }

        // Actualizar los datos permitidos
        usuario.NombreUsuario = dto.NombreUsuario;
        usuario.Correo = correo;
        usuario.Estado = dto.Estado;

        // Guardar los cambios
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        // Buscar el usuario por su ID
        var usuario = await _context.Usuarios.FindAsync(id);

        if (usuario == null)
        {
            return NotFound(new
            {
                mensaje = "No se encontró el usuario."
            });
        }

        // Marcar el usuario para su eliminación
        _context.Usuarios.Remove(usuario);

        // Aplicar los cambios en PostgreSQL
        await _context.SaveChangesAsync();

        return NoContent();
    }

}
