using Domain.Usuarios;
using Domain.Usuarios.Endereco;
using Domain.Usuarios.Parameters;
using Domain.Usuarios.Repository;
using Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infra.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly UsuariosContext _context;

        // Usuario

        public UsuarioRepository(UsuariosContext context)
        {
            _context = context;
        }

        public void Cadastrar(Usuario usuario)
        {
            usuario.Id = Guid.NewGuid();
            _context.Usuarios.Add(usuario);
        }

        public Usuario RetornarPorId(Guid usuarioId)
        {
            return _context.Usuarios.FirstOrDefault(x => x.Id == usuarioId);
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
            return _context.Usuarios
                .OrderBy(x => x.Nome)
                .ThenBy(x => x.Sobrenome)
                .ToList();
        }

        public bool UsuarioExists(Guid usuarioId)
        {
            return _context.Usuarios.Any(x => x.Id == usuarioId);
        }

        // Endereco

        public IEnumerable<UsuarioEndereco> ListarEnderecosPorUsuario(Guid usuarioId)
        {
            return _context.UsuariosEnderecos
                .Where(x => x.UsuarioId == usuarioId)
                .OrderBy(x => x.Estado)
                .ToList();
        }

        public UsuarioEndereco RetornarEnderecoPorId(Guid usuarioId, Guid enderecoId)
        {
            return _context.UsuariosEnderecos.FirstOrDefault(x => x.UsuarioId == usuarioId && x.Id == usuarioId);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}