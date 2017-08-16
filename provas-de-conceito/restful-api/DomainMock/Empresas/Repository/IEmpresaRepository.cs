using Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Empresas.Repository
{
    public interface IEmpresaRepository
    {
        bool Cadastrar(Empresa entidade);
        Empresa RetornarPorId(Guid id);
        bool Atualizar(Empresa entidade);
        bool Remover(Guid id);
        PartialResult<Empresa> Listar(int page = 1, int limit = 10);
    }
}