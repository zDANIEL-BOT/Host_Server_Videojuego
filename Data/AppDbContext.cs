
using Microsoft.EntityFrameworkCore;
using HostingServidoresVideojuegos.Models;

namespace HostingServidoresVideojuegos.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("usuarios");

            entity.HasKey(u => u.IdUsuario);

            entity.Property(u => u.IdUsuario)
                .HasColumnName("id_usuario")
                .ValueGeneratedOnAdd();

            entity.Property(u => u.NombreUsuario)
                .HasColumnName("nombre_usuario")
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(u => u.Correo)
                .HasColumnName("correo")
                .HasMaxLength(150)
                .IsRequired();

            entity.HasIndex(u => u.Correo)
                .IsUnique();

            entity.Property(u => u.PasswordHash)
                .HasColumnName("password_hash")
                .HasMaxLength(255)
                .IsRequired();

            entity.Property(u => u.Estado)
                .HasColumnName("estado")
                .HasDefaultValue(true)
                .IsRequired();

            entity.Property(u => u.FechaCreacion)
                .HasColumnName("fecha_creacion")
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();
        });
    }
}
