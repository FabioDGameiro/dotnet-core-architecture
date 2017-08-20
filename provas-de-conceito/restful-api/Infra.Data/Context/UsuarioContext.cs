using System;
using System.Collections.Generic;
using System.Text;
using Domain.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Context
{
    public class UsuarioContext : DbContext
    {
        public UsuarioContext(DbContextOptions<UsuarioContext> options)
            :base (options)
        {
            Database.Migrate();
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioEndereco> UsuariosEnderecos { get; set; }
    }
}
