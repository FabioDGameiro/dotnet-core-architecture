using Domain.Base;
using Domain.Usuarios.Endereco;
using Domain.Usuarios.Parameters;
using System;
using System.Collections.Generic;

namespace Domain.Usuarios.Repository
{
    public interface IUsuarioRepository
    {
        void CadastrarUsuario(Usuario usuario);
        Usuario RetornaUsuario(Guid usuarioId);
        void AtualizaUsuario(Usuario usuario);
        void RemoveUsuario(Usuario usuario);
        IPagedList<Usuario> RetornaUsuarios(UsuarioParameters parametros);
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