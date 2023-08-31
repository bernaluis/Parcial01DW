using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Parcial01BM101219.Models;

public partial class Bm101219Context : DbContext
{
    public Bm101219Context()
    {
    }

    public Bm101219Context(DbContextOptions<Bm101219Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Curso> Cursos { get; set; }

    public virtual DbSet<Docente> Docentes { get; set; }

    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    public virtual DbSet<Inscripcione> Inscripciones { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=(local);Initial Catalog=BM101219;Integrated Security=True;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasKey(e => e.CursoId).HasName("PK__Cursos__7E023A37CEE375EF");

            entity.Property(e => e.CursoId).HasColumnName("CursoID");
            entity.Property(e => e.DocenteId).HasColumnName("DocenteID");
            entity.Property(e => e.NombreCurso)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Docente).WithMany(p => p.Cursos)
                .HasForeignKey(d => d.DocenteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cursos__DocenteI__398D8EEE");
        });

        modelBuilder.Entity<Docente>(entity =>
        {
            entity.HasKey(e => e.DocenteId).HasName("PK__Docentes__9CB7A941B94DF90E");

            entity.Property(e => e.DocenteId).HasColumnName("DocenteID");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.EstudianteId).HasName("PK__Estudian__6F76833812196699");

            entity.Property(e => e.EstudianteId).HasColumnName("EstudianteID");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Estudiantes)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Estudiant__Usuar__3F466844");
        });

        modelBuilder.Entity<Inscripcione>(entity =>
        {
            entity.HasKey(e => e.InscripcionId).HasName("PK__Inscripc__16831699893BFCDD");

            entity.Property(e => e.InscripcionId).HasColumnName("InscripcionID");
            entity.Property(e => e.CursoId).HasColumnName("CursoID");
            entity.Property(e => e.EstudianteId).HasColumnName("EstudianteID");

            entity.HasOne(d => d.Curso).WithMany(p => p.Inscripciones)
                .HasForeignKey(d => d.CursoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inscripci__Curso__4316F928");

            entity.HasOne(d => d.Estudiante).WithMany(p => p.Inscripciones)
                .HasForeignKey(d => d.EstudianteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inscripci__Estud__4222D4EF");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuarios__2B3DE79879C24661");

            entity.HasIndex(e => e.Email, "UQ__Usuarios__A9D105347B95C18C").IsUnique();

            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Password)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
