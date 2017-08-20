using System;
using Domain.Base;
using Domain.Usuarios.Parameters;

namespace Domain.Usuarios.Repository
{
    public interface IUsuarioRepository
    {
        bool Cadastrar(Usuario entidade);
        Usuario RetornarPorId(Guid id);
        bool Atualizar(Usuario entidade);
        bool Remover(Guid id);
        PartialResult<Usuario> Listar(UsuarioParameters parameters);
    }
}