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

        public void CadastrarUsuario(Usuario usuario)
        {
            usuario.Id = Guid.NewGuid();

            if (usuario.Enderecos.Any())
            {
                foreach (var endereco in usuario.Enderecos)
                {
                    endereco.Id = Guid.NewGuid();
                }
            }

            _context.Usuarios.Add(usuario);
        }

        public Usuario RetornaUsuario(Guid usuarioId)
        {
            return _context.Usuarios.FirstOrDefault(x => x.Id == usuarioId);
        }

        public void AtualizaUsuario(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
        }

        public void RemoveUsuario(Usuario usuario)
        {
            _context.Usuarios.Remove(usuario);
        }

        public IEnumerable<Usuario> RetornaUsuarios(UsuarioParameters parametros)
        {
            return _context.Usuarios
                .OrderBy(x => x.Nome)
                .ThenBy(x => x.Sobrenome)
                .ToList();
        }

        public IEnumerable<Usuario> RetornaUsuarios(IEnumerable<Guid> ids)
        {
            return _context.Usuarios.Where(a => ids.Contains(a.Id))
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

        public UsuarioEndereco RetornarEndereco(Guid usuarioId, Guid enderecoId)
        {
            return _context.UsuariosEnderecos.FirstOrDefault(x => x.UsuarioId == usuarioId && x.Id == enderecoId);
        }

        public void CadastrarEnderecoPorUsuario(Guid usuarioId, UsuarioEndereco endereco)
        {
            var usuario = RetornaUsuario(usuarioId);

            if (usuario == null)
                throw new Exception("Usuario não encontrado");

            endereco.Id = Guid.NewGuid();
            usuario.Enderecos.Add(endereco);
        }

        public void RemoveEndereco(UsuarioEndereco endereco)
        {
            _context.UsuariosEnderecos.Remove(endereco);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

    }
}