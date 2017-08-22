using Domain.Usuarios.Endereco;
using Domain.Usuarios.Parameters;
using System;
using System.Collections.Generic;

namespace Domain.Usuarios.Repository
{
    public interface IUsuarioRepository
    {
        void Cadastrar(Usuario usuario);
        Usuario RetornarPorId(Guid usuarioId);
        void Atualizar(Usuario usuario);
        void Remover(Usuario usuario);
        IEnumerable<Usuario> Listar(UsuarioParameters parametros);
        IEnumerable<Usuario> RetornaUsuarios(IEnumerable<Guid> ids);
        bool UsuarioExists(Guid usuarioId);

        IEnumerable<UsuarioEndereco> ListarEnderecosPorUsuario(Guid usuarioId);
        UsuarioEndereco RetornarEnderecoPorId(Guid usuarioId, Guid enderecoId);
        void CadastrarEnderecoPorUsuario(Guid usuarioId, UsuarioEndereco endereco);


        bool Save();
        
    }
}