using System;
using Domain.Usuarios.Parameters;
using System.Collections.Generic;

namespace Domain.Usuarios.Repository
{
    public interface IUsuarioRepository
    {
        void Cadastrar(Usuario usuario);
        Usuario RetornarPorId(Guid id);
        void Atualizar(Usuario usuario);
        void Remover(Usuario usuario);
        IEnumerable<Usuario> Listar(UsuarioParameters parametros);
        bool Save();
    }
}