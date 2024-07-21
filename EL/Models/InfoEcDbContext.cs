using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace INFO_EC_BACKEND.Models;

public partial class InfoEcDbContext : DbContext
{
    public InfoEcDbContext()
    {
    }

    public InfoEcDbContext(DbContextOptions<InfoEcDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Cuentum> Cuenta { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__CATEGORI__CB9033494C486FF3");

            entity.ToTable("CATEGORIA");

            entity.Property(e => e.IdCategoria).HasColumnName("Id_Categoria");
            entity.Property(e => e.NombreCategoria)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Nombre_Categoria");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__CLIENTE__3DD0A8CB5AF0AD36");

            entity.ToTable("CLIENTE");

            entity.Property(e => e.IdCliente).HasColumnName("Id_Cliente");
            entity.Property(e => e.IdCuenta).HasColumnName("Id_Cuenta");

            entity.HasOne(d => d.IdCuentaNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdCuenta)
                .HasConstraintName("FK__CLIENTE__Id_Cuen__3B75D760");
        });

        modelBuilder.Entity<Cuentum>(entity =>
        {
            entity.HasKey(e => e.IdCuenta).HasName("PK__CUENTA__462699D8F38CB97E");

            entity.ToTable("CUENTA");

            entity.Property(e => e.IdCuenta).HasColumnName("Id_Cuenta");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Cedula)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PK__EMPLEADO__01AC28298064DF31");

            entity.ToTable("EMPLEADO");

            entity.Property(e => e.IdEmpleado).HasColumnName("Id_empleado");
            entity.Property(e => e.Clave).IsUnicode(false);
            entity.Property(e => e.CuentaIdCuenta).HasColumnName("Cuenta_IdCuenta");
            entity.Property(e => e.RolIdRol).HasColumnName("Rol_IdRol");

            entity.HasOne(d => d.CuentaIdCuentaNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.CuentaIdCuenta)
                .HasConstraintName("FK__EMPLEADO__Cuenta__403A8C7D");

            entity.HasOne(d => d.RolIdRolNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.RolIdRol)
                .HasConstraintName("FK__EMPLEADO__Rol_Id__3F466844");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.IdMarca).HasName("PK__MARCA__28EFE28AAA84F726");

            entity.ToTable("MARCA");

            entity.Property(e => e.IdMarca).HasColumnName("Id_Marca");
            entity.Property(e => e.NombreMarca)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Nombre_Marca");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__PRODUCTO__2085A9CF3872C774");

            entity.ToTable("PRODUCTO");

            entity.Property(e => e.IdProducto).HasColumnName("Id_Producto");
            entity.Property(e => e.CategoriaIdCategoria).HasColumnName("Categoria_IdCategoria");
            entity.Property(e => e.Imagen)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(6, 2)");

            entity.HasOne(d => d.CategoriaIdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.CategoriaIdCategoria)
                .HasConstraintName("FK__PRODUCTO__Catego__46E78A0C");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__ROL__55932E86420263C8");

            entity.ToTable("ROL");

            entity.Property(e => e.IdRol).HasColumnName("Id_Rol");
            entity.Property(e => e.NombreRol)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Nombre_Rol");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
