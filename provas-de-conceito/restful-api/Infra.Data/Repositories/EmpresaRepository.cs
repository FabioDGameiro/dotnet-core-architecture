using Domain.Base;
using Domain.Empresas;
using Domain.Empresas.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infra.Data.Repositories
{
    public class EmpresaRepository : IEmpresaRepository
    {
        public static List<Empresa> _empresasList = new List<Empresa>();

        public EmpresaRepository()
        {
            // mock
            if (_empresasList.Count() == 0)
            {
                _empresasList.Add(new Empresa
                {
                    Id = new Guid("d36e2feb-999f-4f92-8ce5-5e83d4be6467"),
                    Cnpj = "18465042000163",
                    RazaoSocial = "Empresa Teste 001",
                    NomeFantasia = "Empresa 01",
                    SocioProprietario = "Tiago",
                    Ramo = "TI",
                    Categoria = "Desenvolvimento"
                });

                _empresasList.Add(new Empresa
                {
                    Id = new Guid("830cf298-6960-4561-a12f-69ed08e06c89"),
                    Cnpj = "45054445000192",
                    RazaoSocial = "Empresa Teste 002",
                    NomeFantasia = "Empresa 02",
                    SocioProprietario = "Renan",
                    Ramo = "TI",
                    Categoria = "Jogos"
                });

                _empresasList.Add(new Empresa
                {
                    Id = new Guid("3cfe0e64-3728-4414-b8f8-6b48b7c4e9fc"),
                    Cnpj = "32502841000193",
                    RazaoSocial = "Empresa Teste 003",
                    NomeFantasia = "Empresa 03",
                    SocioProprietario = "Wellington",
                    Ramo = "TI",
                    Categoria = "Jogos"
                });

                _empresasList.Add(new Empresa
                {
                    Id = new Guid("7ff44178-5249-40a9-9422-015125e8f835"),
                    Cnpj = "12473757000181",
                    RazaoSocial = "Empresa Teste 004",
                    NomeFantasia = "Empresa 04",
                    SocioProprietario = "Vinicius",
                    Ramo = "Seguros",
                    Categoria = "Seguro Automoveis"
                });

                _empresasList.Add(new Empresa
                {
                    Id = new Guid("76f5a981-dde2-4ba8-acda-8177dcc0cdb9"),
                    Cnpj = "73610755000181",
                    RazaoSocial = "Empresa Teste 005",
                    NomeFantasia = "Empresa 05",
                    SocioProprietario = "Marcos",
                    Ramo = "Seguros",
                    Categoria = "Seguro de Vida"
                });
            }
        }

        public PartialResult<Empresa> Listar(
            Func<Empresa, bool> predicate = null, int page = 1, int limit = 10, bool metaonly = false)
        {
            var partialResult = new PartialResult<Empresa>(page, limit);
            IEnumerable<Empresa> empresasList = _empresasList;

            if (predicate != null)
                empresasList = empresasList.Where(predicate);

            partialResult.Count = empresasList.Count();

            if (metaonly) return partialResult;

            partialResult.Data = empresasList.Skip((page - 1) * limit).Take(limit);
            return partialResult;
        }

        public bool Cadastrar(Empresa entidade)
        {
            _empresasList.Add(entidade);
            return true;
        }

        public bool Atualizar(Empresa entidade)
        {
            var index = _empresasList.FindIndex(x => x.Id == entidade.Id);
            _empresasList[index] = entidade;
            return true;
        }

        public Empresa RetornarPorId(Guid id)
        {
            return _empresasList.FirstOrDefault(x => x.Id == id);
        }

        public bool Remover(Guid id)
        {
            return _empresasList.Remove(RetornarPorId(id));
        }
    }
}
