using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MS_Facturacion.Models
{
    public partial class MS_FacturacionContext : DbContext
    {
        public MS_FacturacionContext()
        {
        }

        public MS_FacturacionContext(DbContextOptions<MS_FacturacionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DetalleFactura> DetalleFactura { get; set; }
        public virtual DbSet<EstadoFactura> EstadoFactura { get; set; }
        public virtual DbSet<Factura> Factura { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-B95M12V\\SQLEXPRESS;Database=MS_Facturacion;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DetalleFactura>(entity =>
            {
                entity.ToTable("detalle_factura");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CantidadProducto).HasColumnName("cantidad_producto");

                entity.Property(e => e.FacturaId).HasColumnName("factura_id");

                entity.Property(e => e.Iva)
                    .HasColumnName("iva")
                    .HasColumnType("decimal(15, 2)");

                entity.Property(e => e.NombreProducto)
                    .HasColumnName("nombre_producto")
                    .HasMaxLength(100);

                entity.Property(e => e.ProductoId).HasColumnName("producto_id");

                entity.Property(e => e.ValorTotal)
                    .HasColumnName("valor_total")
                    .HasColumnType("decimal(15, 2)");

                entity.Property(e => e.ValorUnitario)
                    .HasColumnName("valor_unitario")
                    .HasColumnType("decimal(15, 2)");

                entity.HasOne(d => d.Factura)
                    .WithMany(p => p.DetalleFactura)
                    .HasForeignKey(d => d.FacturaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_factura_id");
            });

            modelBuilder.Entity<EstadoFactura>(entity =>
            {
                entity.ToTable("estado_factura");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descripcion)
                    .HasColumnName("descripcion")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.ToTable("factura");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.EstadoId).HasColumnName("estado_id");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ganancia).HasColumnName("ganancia");

                entity.Property(e => e.IdCliente)
                    .HasColumnName("id_cliente")
                    .HasMaxLength(20);

                entity.Property(e => e.Iva)
                    .HasColumnName("iva")
                    .HasColumnType("decimal(15, 2)");

                entity.Property(e => e.Neto)
                    .HasColumnName("neto")
                    .HasColumnType("decimal(15, 2)");

                entity.Property(e => e.NombreCliente)
                    .HasColumnName("nombre_cliente")
                    .HasMaxLength(100);

                entity.Property(e => e.NroFactura).HasColumnName("nro_factura");

                entity.Property(e => e.ValorTotal)
                    .HasColumnName("valor_total")
                    .HasColumnType("decimal(15, 2)");

                entity.HasOne(d => d.Estado)
                    .WithMany(p => p.Factura)
                    .HasForeignKey(d => d.EstadoId)
                    .HasConstraintName("FK_estado_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
