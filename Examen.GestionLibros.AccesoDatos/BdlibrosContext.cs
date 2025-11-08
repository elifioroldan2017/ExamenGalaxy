using System;
using System.Collections.Generic;
using Examen.GestionLibros.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Examen.GestionLibros.AccesoDatos;

public partial class BdlibrosContext : DbContext
{
    public BdlibrosContext()
    {
    }

    public BdlibrosContext(DbContextOptions<BdlibrosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Autor> Autors { get; set; }

    public virtual DbSet<Libro> Libros { get; set; }

    public virtual DbSet<TipoLibro> TipoLibros { get; set; }

 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Autor__3214EC0757B5266F");

            entity.ToTable("Autor");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Apellidos)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nacionalidad)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombres)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Libro__3214EC0729FFDCAF");

            entity.ToTable("Libro");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Isbn)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ISBN");
            entity.Property(e => e.Stock).HasDefaultValue(0);
            entity.Property(e => e.Titulo)
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.HasOne(d => d.Autor).WithMany(p => p.Libros)
                .HasForeignKey(d => d.AutorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Libro__AutorId__2E1BDC42");

            entity.HasOne(d => d.TipoLibro).WithMany(p => p.Libros)
                .HasForeignKey(d => d.TipoLibroId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Libro__TipoLibro__2D27B809");
        });

        modelBuilder.Entity<TipoLibro>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TipoLibr__3214EC070D4832A8");

            entity.ToTable("TipoLibro");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
