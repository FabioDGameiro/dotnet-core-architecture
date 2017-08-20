using System;
using Domain.Base;
using Domain.Usuarios;
using Domain.Usuarios.Parameters;
using Domain.Usuarios.Repository;

namespace Infra.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public bool Cadastrar(Usuario entidade)
        {
            throw new NotImplementedException();
        }

        public Usuario RetornarPorId(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Atualizar(Usuario entidade)
        {
            throw new NotImplementedException();
        }

        public bool Remover(Guid id)
        {
            throw new NotImplementedException();
        }

        public PartialResult<Usuario> Listar(UsuarioParameters parameters)
        {
            throw new NotImplementedException();
        }
    }
}