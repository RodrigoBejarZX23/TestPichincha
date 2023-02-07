using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestPichincha.Models;

namespace TestPichincha.DBContexts
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options)
        { 
        
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { 
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.Entity<Persona>().ToTable("Persona");
            modelBuilder.Entity<Cliente>().ToTable("Cliente");
            modelBuilder.Entity<Cuenta>().ToTable("Cuenta");
            modelBuilder.Entity<Movimiento>().ToTable("Movimiento");

            modelBuilder.Entity<Persona>().HasKey(p => p.PersonaId).HasName("PersonaId");
            modelBuilder.Entity<Cliente>().HasKey(c => c.ClienteId).HasName("ClienteId");
            modelBuilder.Entity<Cuenta>().HasKey(cu => cu.CuentaId).HasName("CuentaId");
            modelBuilder.Entity<Movimiento>().HasKey(m => m.MovimientoId).HasName("MovimientoId");

            modelBuilder.Entity<Persona>().Property(p => p.PersonaId).HasColumnType("int").IsRequired();
            modelBuilder.Entity<Persona>().Property(p => p.Nombre).HasColumnType("nvarchar(100)").IsRequired();
            modelBuilder.Entity<Persona>().Property(p => p.Genero).HasColumnType("nvarchar(100)").IsRequired();
            modelBuilder.Entity<Persona>().Property(p => p.Edad).HasColumnType("int").IsRequired();
            modelBuilder.Entity<Persona>().Property(p => p.Identificacion).HasColumnType("int").IsRequired();
            modelBuilder.Entity<Persona>().Property(p => p.Direccion).HasColumnType("nvarchar(100)").IsRequired();
            modelBuilder.Entity<Persona>().Property(p => p.Telefono).HasColumnType("nvarchar(100)").IsRequired();

            modelBuilder.Entity<Cliente>().Property(c => c.ClienteId).HasColumnType("int").IsRequired();
            modelBuilder.Entity<Cliente>().Property(c => c.Contrasenia).HasColumnType("nvarchar(100)").IsRequired();
            modelBuilder.Entity<Cliente>().Property(c => c.Estado).HasColumnType("bool").IsRequired();
            modelBuilder.Entity<Cliente>().Property(c => c.PersonaId).HasColumnType("int").IsRequired();

            modelBuilder.Entity<Cuenta>().Property(cu => cu.CuentaId).HasColumnType("int").IsRequired();
            modelBuilder.Entity<Cuenta>().Property(cu => cu.NumeroCuenta).HasColumnType("int").IsRequired();
            modelBuilder.Entity<Cuenta>().Property(cu => cu.TipoCuenta).HasColumnType("nvarchar(100)").IsRequired();
            modelBuilder.Entity<Cuenta>().Property(cu => cu.SaldoInicial).HasColumnType("int").IsRequired();
            modelBuilder.Entity<Cuenta>().Property(cu => cu.Estado).HasColumnType("bool").IsRequired();
            modelBuilder.Entity<Cliente>().Property(cu => cu.ClienteId).HasColumnType("int").IsRequired();

            modelBuilder.Entity<Movimiento>().Property(m => m.CuentaId).HasColumnType("int").IsRequired();
            modelBuilder.Entity<Movimiento>().Property(m => m.Fecha).HasColumnType("Datetime").IsRequired();
            modelBuilder.Entity<Movimiento>().Property(m => m.TipoMovimiento).HasColumnType("nvarchar(100)").IsRequired();
            modelBuilder.Entity<Movimiento>().Property(m => m.Valor).HasColumnType("int").IsRequired();
            modelBuilder.Entity<Movimiento>().Property(m => m.Saldo).HasColumnType("int").IsRequired();
            modelBuilder.Entity<Movimiento>().Property(m => m.CuentaId).HasColumnType("int").IsRequired();

            // Configure relationships  
            modelBuilder.Entity<Cliente>().
                HasOne(x => x.Persona).
                WithOne().
                HasForeignKey<Cliente>(fk => fk.PersonaId).
                OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_Cliente_Persona");

            modelBuilder.Entity<Cuenta>().
                HasOne(x=>x.Cliente).
                WithOne().
                HasForeignKey<Cuenta>(fk => fk.ClienteId).
                OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_Cuenta_Cliente");

            modelBuilder.Entity<Movimiento>().
               HasOne(x => x.Cuenta).
               WithOne().
               HasForeignKey<Movimiento>(fk => fk.CuentaId).
               OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_Movimiento_Cuenta");

        }

        public DbSet<Persona> Personas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<Movimiento> Movimientos { get; set; }

    }
}
