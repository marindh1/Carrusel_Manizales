using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MS_PQR.Models
{
    public partial class MS_PQRContext : DbContext
    {
        public MS_PQRContext()
        {
        }

        public MS_PQRContext(DbContextOptions<MS_PQRContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Pqr> Pqr { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost\\sqlexpress;Initial Catalog=MS_PQR;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pqr>(entity =>
            {
                entity.ToTable("PQR");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("descripcion")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .HasColumnName("timestamp")
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasColumnName("tipo")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
