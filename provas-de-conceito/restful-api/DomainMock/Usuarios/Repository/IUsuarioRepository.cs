using System;
using Domain.Usuarios.Parameters;
using System.Collections.Generic;
using Domain.Usuarios.Endereco;

namespace Domain.Usuarios.Repository
{
    public interface IUsuarioRepository
    {
        void Cadastrar(Usuario usuario);
        Usuario RetornarPorId(Guid usuarioId);
        void Atualizar(Usuario usuario);
        void Remover(Usuario usuario);
        IEnumerable<Usuario> Listar(UsuarioParameters parametros);
        bool UsuarioExists(Guid usuarioId);

        IEnumerable<UsuarioEndereco> ListarEnderecosPorUsuario(Guid usuarioId);
        UsuarioEndereco RetornarEnderecoPorId(Guid usuarioId, Guid enderecoId);

        bool Save();
    }
}