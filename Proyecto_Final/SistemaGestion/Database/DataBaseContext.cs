﻿using Microsoft.EntityFrameworkCore;
using SistemaGestion.Models;

namespace SistemaGestion.Database
{
    public partial class DataBaseContext : DbContext
    {
        public DataBaseContext() 
        { 
        
        }

        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) 
        {        
        }

        public virtual DbSet<Producto> Producto { get; set; } = null!;
        public virtual DbSet<ProductoVendido> ProductoVendidos { get; set; } = null!;
        public virtual DbSet<Usuario> Usuario { get; set; } = null!;
        public virtual DbSet<Venta> Venta { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("Producto");

                entity.Property(e => e.Costo).HasColumnType("money");

                entity.Property(e => e.Descripciones).IsUnicode(false);

                entity.Property(e => e.PrecioVenta).HasColumnType("money");

                entity.Property(e => e.Stock).IsUnicode(false);

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Producto_Usuario");
            });

            modelBuilder.Entity<ProductoVendido>(entity =>
            {
                entity.ToTable("ProductoVendido");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.ProductoVendidos)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ProductoVendido_Producto");

                entity.HasOne(d => d.IdVentaNavigation)
                    .WithMany(p => p.ProductoVendidos)
                    .HasForeignKey(d => d.IdVenta)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ProductoVendido_Venta");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");

                entity.Property(e => e.Apellido).IsUnicode(false);

                entity.Property(e => e.Contraseña).IsUnicode(false);

                entity.Property(e => e.Mail).IsUnicode(false);

                entity.Property(e => e.Nombre).IsUnicode(false);

                entity.Property(e => e.NombreUsuario).IsUnicode(false);
            });

            modelBuilder.Entity<Venta>(entity =>
            {
                entity.Property(e => e.Comentarios).IsUnicode(false);

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Venta_Usuario");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
