#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain.Base;
using Domain.Usuarios;
using Domain.Usuarios.Enderecos;
using Domain.Usuarios.Parameters;
using Domain.Usuarios.Repository;
using Infra.Data.Context;
using Infra.Helpers;

#endregion

namespace Infra.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly UsuariosContext _context;

        public UsuarioRepository(UsuariosContext context)
        {
            _context = context;
        }

        // Usuario

        public void CadastrarUsuario(Usuario usuario)
        {
            usuario.Id = Guid.NewGuid();

            if (usuario.Enderecos.Any())
                foreach (var endereco in usuario.Enderecos)
                    endereco.Id = Guid.NewGuid();

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

        public IPagedList<Usuario> RetornaUsuarios(Specification<Usuario> specification, string orderBy, int page,
            int pageSize, bool metaOnly)
        {
            var usuariosQuery = _context.Usuarios
                .Where(specification.ToExpression()).AsQueryable();

            // ordenação

            usuariosQuery = usuariosQuery.ApplyOrdering(orderBy);


            // retorno paginado

            return PagedList<Usuario>.Create(usuariosQuery, page, pageSize, metaOnly);
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

        public IPagedList<UsuarioEndereco> ListarEnderecosPorUsuario(UsuarioEnderecoParameters parametros)
        {
            var enderecosQuery = _context.UsuariosEnderecos.Where(x => x.UsuarioId == parametros.UsuarioId);

            // ordenação

            //enderecosQuery = AplicaOrdenacaoEnderecosUsuario(enderecosQuery, parametros.OrderBy);

            //enderecosQuery = enderecosQuery.OrderBy(parametros.OrderBy);

            // retorno paginado

            return PagedList<UsuarioEndereco>.Create(enderecosQuery, parametros.Page, parametros.PageSize,
                parametros.MetaOnly);
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

        private IQueryable<Usuario> AplicaOrdenacaoUsuarios(IQueryable<Usuario> usuarios, string orderBy)
        {
            // Caso não houver nenhuma ordenação no parametro
            // aplica a ordenação padrão
            if (string.IsNullOrWhiteSpace(orderBy))
                return usuarios.OrderBy(x => x.Nome).ThenBy(x => x.Sobrenome);

            var orderQuery = usuarios.OrderBy(x => 0);
            foreach (var order in orderBy.Split(','))
                switch (order)
                {
                    case "nome":
                        orderQuery = orderQuery.ThenBy(x => x.Nome).ThenBy(x => x.Sobrenome);
                        break;

                    case "nome-desc":
                        orderQuery = orderQuery.ThenByDescending(x => x.Nome).ThenByDescending(x => x.Sobrenome);
                        break;

                    case "idade":
                        orderQuery = orderQuery.ThenByDescending(x => x.DataNascimento);
                        break;

                    case "idade-desc":
                        orderQuery = orderQuery.ThenBy(x => x.DataNascimento);
                        break;

                    case "sexo":
                        orderQuery = orderQuery.ThenBy(x => x.Sexo);
                        break;

                    case "sexo-desc":
                        orderQuery = orderQuery.ThenByDescending(x => x.Sexo);
                        break;

                    default:
                        orderQuery = orderQuery.ThenBy(x => x.Nome).ThenBy(x => x.Sobrenome);
                        break;
                }

            return orderQuery.AsQueryable();
        }

        private IQueryable<UsuarioEndereco> AplicaOrdenacaoEnderecosUsuario(IQueryable<UsuarioEndereco> enderecos,
            string orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy))
                return enderecos.OrderBy(x => x.Estado).ThenBy(x => x.Tipo);

            var orderQuery = enderecos.OrderBy(x => 0);
            foreach (var order in orderBy.Split(','))
                switch (order)
                {
                    case "tipo":
                        orderQuery = orderQuery.ThenBy(x => x.Tipo);
                        break;

                    case "tipo-desc":
                        orderQuery = orderQuery.ThenByDescending(x => x.Tipo);
                        break;

                    case "estado":
                        orderQuery = orderQuery.ThenBy(x => x.Estado);
                        break;

                    case "estado-desc":
                        orderQuery = orderQuery.ThenByDescending(x => x.Estado);
                        break;
                }

            return orderQuery.AsQueryable();
        }
    }

    

}
