#region Using

using System;
using System.Collections.Generic;
using Domain.Base;
using Domain.Usuarios.Enderecos;
using Domain.Usuarios.Parameters;

#endregion

namespace Domain.Usuarios.Repository
{
    public interface IUsuarioRepository
    {
        void CadastrarUsuario(Usuario usuario);
        Usuario RetornaUsuario(Guid usuarioId);
        void AtualizaUsuario(Usuario usuario);
        void RemoveUsuario(Usuario usuario);

        IPagedList<Usuario> RetornaUsuarios(Specification<Usuario> specification, string orderBy, int page,
            int pageSize, bool metaOnly);

        IEnumerable<Usuario> RetornaUsuarios(IEnumerable<Guid> ids);
        bool UsuarioExists(Guid usuarioId);
        bool EmailExists(string email, Guid usuarioExceptionId = default(Guid));

        IPagedList<UsuarioEndereco> ListarEnderecosPorUsuario(UsuarioEnderecoParameters parametros);
        UsuarioEndereco RetornarEndereco(Guid usuarioId, Guid enderecoId);
        void CadastrarEnderecoPorUsuario(Guid usuarioId, UsuarioEndereco endereco);
        void RemoveEndereco(UsuarioEndereco endereco);
        bool EnderecoExists(Guid usuarioId, Guid enderecoId);
        void AtualizaUsuarioEndereco(UsuarioEndereco usuario);

        bool Save();
    }
}