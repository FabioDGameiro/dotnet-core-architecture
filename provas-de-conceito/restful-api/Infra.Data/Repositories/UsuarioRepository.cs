using System;
using Domain.Usuarios;
using Domain.Usuarios.Parameters;
using Domain.Usuarios.Repository;
using Infra.Data.Context;
using System.Linq;
using System.Collections.Generic;

namespace Infra.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly UsuariosContext _context;

        public UsuarioRepository(UsuariosContext context)
        {
            _context = context;
        }

        public void Cadastrar(Usuario usuario)
        {
            usuario.Id = Guid.NewGuid();
            _context.Usuarios.Add(usuario);
        }

        public Usuario RetornarPorId(Guid id)
        {
            return _context.Usuarios.FirstOrDefault(x => x.Id == id);
        }

        public void Atualizar(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
        }

        public void Remover(Usuario usuario)
        {
            _context.Usuarios.Remove(usuario);
        }

        public IEnumerable<Usuario> Listar(UsuarioParameters parametros)
        {
            return _context.Usuarios.OrderBy(x => x.Nome).ThenBy(x => x.Sobrenome).ToList();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}