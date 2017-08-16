using AutoMapper;
using Domain.Empresas;
using Domain.Empresas.Repository;
using Microsoft.AspNetCore.Mvc;
using RestfulAPI.Controllers.Base;
using RestfulAPI.Models.Empresa;
using System;

namespace RestfulAPI.Controllers.Empresas
{
    [Route("api/empresas")]
    public class EmpresaController : BaseController
    {
        private readonly IEmpresaRepository _empresaRepository;
        private readonly IMapper _mapper;

        public EmpresaController(IEmpresaRepository empresaRepository, IMapper mapper)
        {
            _empresaRepository = empresaRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Get(EmpresaFilterModel filter)
        {
            var entitesList = _empresaRepository.Listar();
            var modelsList = _mapper.Map<EmpresaItemModel>(entitesList);

            return Ok(modelsList);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult Get(Guid id)
        {
            return Ok();
        }

        [HttpPost]
        [Route("")]
        public IActionResult Post(EmpresaModel model)
        {
            _empresaRepository.Cadastrar(new Empresa { NomeFantasia = "Teste" });
            return Ok();
        }

        [HttpPut]
        [Route("")]
        public IActionResult Put(EmpresaModel model)
        {
            return Ok();
        }

        // TODO: ver como funciona esse método PATCH
        [HttpPut]
        [Route("")]
        public IActionResult Patch()
        {
            return Ok();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            return Ok();
        }
    }
}