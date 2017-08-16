using Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Domain.Empresas.Repository
{
    public interface IEmpresaRepository
    {
        bool Cadastrar(Empresa entidade);
        Empresa RetornarPorId(Guid id);
        bool Atualizar(Empresa entidade);
        bool Remover(Guid id);
        PartialResult<Empresa> Listar(Func<Empresa, bool> predicate = null, int page = 1, int limit = 10, bool metaonly = false);
    }
}