using Domain.Base;
using Domain.Usuarios;
using Domain.Usuarios.Endereco;
using Domain.Usuarios.Parameters;
using Domain.Usuarios.Repository;
using Infra.Data.Context;
using Infra.Helpers;
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

        public IPagedList<Usuario> RetornaUsuarios(UsuarioParameters parametros)
        {
            var usuariosQuery = _context.Usuarios
                .OrderBy(x => x.Nome)
                .ThenBy(x => x.Sobrenome)
                .AsQueryable();

            if (parametros.Sexo.HasValue)
                usuariosQuery = usuariosQuery.Where(x => x.Sexo == parametros.Sexo);

            if (!string.IsNullOrWhiteSpace(parametros.Email))
                usuariosQuery = usuariosQuery.Where(x => x.Email.ToLowerInvariant() == parametros.Email.ToLowerInvariant());

            if (parametros.HasQuery)
            {
                usuariosQuery = usuariosQuery.Where(x => 
                    x.Nome.ToLowerInvariant().Contains(parametros.Query) ||
                    x.Sobrenome.ToLowerInvariant().Contains(parametros.Query) ||
                    x.Email.ToLowerInvariant().Contains(parametros.Query)
                    );
            }

            return PagedList<Usuario>.Create(
                usuariosQuery,
                parametros.Page,
                parametros.PageSize);
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

        public bool EmailExists(string email, Guid usuarioExceptionId = default(Guid))
        {
            return _context.Usuarios.Any(x => x.Email == email && x.Id != usuarioExceptionId);
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

        public bool EnderecoExists(Guid usuarioId, Guid enderecoId)
        {
            return _context.UsuariosEnderecos.Any(x => x.UsuarioId == usuarioId && x.Id == enderecoId);
        }

        public void AtualizaUsuarioEndereco(UsuarioEndereco usuario)
        {
            _context.UsuariosEnderecos.Update(usuario);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}