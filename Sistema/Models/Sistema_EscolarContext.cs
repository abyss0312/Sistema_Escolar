using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Sistema.Models
{
    public partial class Sistema_EscolarContext : DbContext
    {
        public Sistema_EscolarContext()
        {
        }

        public Sistema_EscolarContext(DbContextOptions<Sistema_EscolarContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Alumnos> Alumnos { get; set; }
        public virtual DbSet<Materia> Materia { get; set; }
        public virtual DbSet<Profesores> Profesores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-415LOUM;Database=Sistema_Escolar;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alumnos>(entity =>
            {
                entity.HasKey(e => e.AlumnoId);

                entity.Property(e => e.AlumnoId).HasColumnName("AlumnoID");

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Edad).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Pass)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.MateriaNavigation)
                    .WithMany(p => p.Alumnos)
                    .HasForeignKey(d => d.Materia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Alumnos_Materia1");

                entity.HasOne(d => d.ProfesorNavigation)
                    .WithMany(p => p.Alumnos)
                    .HasForeignKey(d => d.Profesor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Alumnos_Profesores");
            });

            modelBuilder.Entity<Materia>(entity =>
            {
                entity.Property(e => e.MateriaId).HasColumnName("MateriaID");

                entity.Property(e => e.NombreMateria)
                    .IsRequired()
                    .HasColumnName("Nombre_Materia")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Profesores>(entity =>
            {
                entity.HasKey(e => e.ProfesorId);

                entity.Property(e => e.ProfesorId).HasColumnName("ProfesorID");

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Edad).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Pass)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.MateriaNavigation)
                    .WithMany(p => p.Profesores)
                    .HasForeignKey(d => d.Materia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Profesores_Materia");
            });
        }
    }
}
