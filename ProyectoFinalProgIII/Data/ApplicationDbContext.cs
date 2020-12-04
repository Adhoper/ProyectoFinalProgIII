using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProyectoFinalProgIII.Data
{
    public class ApplicationDbContext : IdentityDbContext<Usuarios>
    {
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Facturacion> Facturacion { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Servicios> Servicios { get; set; }
        public DbSet<FacturacionProductos> FacturacionProductos { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Usuarios>(entityTypeBuilder => {
                entityTypeBuilder.ToTable("Usuarios");
                entityTypeBuilder.Property(u => u.Nombre).HasMaxLength(100);
                entityTypeBuilder.Property(u => u.NombreComercial).HasMaxLength(100);
                entityTypeBuilder.Property(u => u.CedulaRNC).HasMaxLength(100);
                entityTypeBuilder.Property(u => u.Direccion).HasMaxLength(100);
                entityTypeBuilder.Property(u => u.PaginaWeb).HasMaxLength(100);

            });
        }
    }

    public class Usuarios : IdentityUser
    {
        public string CedulaRNC { get; set; }

        public string Nombre { get; set; }

        public string NombreComercial { get; set; }

        public string Direccion { get; set; }

        public string PaginaWeb { get; set; }

    }

    public class Clientes
    {
        [Key]
        public Guid ClienteId { get; set; }
        public string Nombre { get; set; }
        public string RNC { get; set; }
        public string Direccion { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        [Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }
        public string CantCompras { get; set; }

    }

    public class Facturacion
    {
        public Guid FacturacionId { get; set; }

        public string TipoFactura { get; set; }
        public string Cantidad { get; set; }
        public decimal Itbis { get; set; }
        public decimal Precio { get; set; }
        public Clientes Cliente { get; set; }
        public Guid ClienteId { get; set; }

        public Guid UsuarioId { get; set; }

        public Guid ProductosId { get; set; }
        public Guid ServiciosId { get; set; }
    }

    public class Productos
    {
        public Guid ProductosId { get; set; }
        public string NombreP { get; set; }
        public string Descripcion { get; set; }
        public string Valor { get; set; }
        public string Cantidad { get; set; }

        public string CantVendidos { get; set; }

    }

    public class FacturacionProductos
    {

        public Guid Id { get; set; }
        public Guid ProductosId { get; set; }
        public Guid FacturacionId { get; set; }
        public Productos Productos { get; set; }
        public Facturacion Facturacion { get; set; }
        public decimal PrecioTotal { get; set; }
    }

    public class Servicios
    {
        public Guid ServiciosId { get; set; }

        public string NombreS { get; set; }
        public string Descripcion { get; set; }
        public string Valor { get; set; }

        public string CantVendidos { get; set; }
    }
}
