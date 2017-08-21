using System;
using System.Collections.Generic;
using System.Text;
using Domain.Usuarios;
using Domain.Usuarios.Endereco;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Context
{
    public class UsuariosContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioEndereco> UsuariosEnderecos { get; set; }

        public UsuariosContext(DbContextOptions<UsuariosContext> options)
            :base (options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>().HasKey(x => x.Id);
            modelBuilder.Entity<Usuario>().Property(x => x.Nome).IsRequired().HasColumnType("varchar(20)");
            modelBuilder.Entity<Usuario>().Property(x => x.Sobrenome).IsRequired().HasColumnType("varchar(30)");
            modelBuilder.Entity<Usuario>().Property(x => x.Email).IsRequired().HasColumnType("varchar(30)");
            modelBuilder.Entity<Usuario>().Property(x => x.DataNascimento).IsRequired();

            modelBuilder.Entity<UsuarioEndereco>().HasKey(x => x.Id);
            modelBuilder.Entity<UsuarioEndereco>().Property(x => x.Endereco).IsRequired().HasColumnType("varchar(100)");
            modelBuilder.Entity<UsuarioEndereco>().Property(x => x.Estado).IsRequired().HasColumnType("varchar(2)");
            modelBuilder.Entity<UsuarioEndereco>().Property(x => x.Tipo).IsRequired();
        }
    }
}
