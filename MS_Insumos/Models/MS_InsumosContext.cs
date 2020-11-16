using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MS_Insumos.Models
{
    public partial class MS_InsumosContext : DbContext
    {
        public MS_InsumosContext()
        {
        }

        public MS_InsumosContext(DbContextOptions<MS_InsumosContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categorias> Categorias { get; set; }
        public virtual DbSet<Insumos> Insumos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-B95M12V\\SQLEXPRESS;Database=MS_Insumos;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categorias>(entity =>
            {
                entity.ToTable("categorias");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CatDescripcion)
                    .HasColumnName("cat_descripcion")
                    .HasMaxLength(50);

                entity.Property(e => e.CatEstado).HasColumnName("cat_estado");
            });

            modelBuilder.Entity<Insumos>(entity =>
            {
                entity.ToTable("insumos");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CategoriaId).HasColumnName("categoria_id");

                entity.Property(e => e.Descripcion)
                    .HasColumnName("descripcion")
                    .HasMaxLength(100);

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.HasOne(d => d.Categoria)
                    .WithMany(p => p.Insumos)
                    .HasForeignKey(d => d.CategoriaId)
                    .HasConstraintName("FK_categoria_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
