using AutoMapper;
using Domain.Empresas;
using Domain.Empresas.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestfulAPI.Controllers.Base;
using RestfulAPI.Models.Empresa;
using System;
using System.Collections.Generic;

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
        public IActionResult Get(EmpresaFilterModel filter)
        {
            var partialResult = _empresaRepository.Listar(x =>
                (x.RazaoSocial.ToLower() == filter.RazaoSocial?.ToLower() || filter.RazaoSocial == null) &&
                (x.Ramo.ToLower() == filter.Ramo?.ToLower() || filter.Ramo == null)
            );

            if (partialResult.Data == null) return NotFound(new { message = "Itens não encontrados" });

            var modelsList = _mapper.Map<IEnumerable<EmpresaItemModel>>(partialResult.Data);

            Response.Headers.Add("Pagination-Count", partialResult.Count.ToString());
            Response.Headers.Add("Pagination-Page", partialResult.Page.ToString());
            Response.Headers.Add("Pagination-Limit", partialResult.Limit.ToString());

            return Ok(modelsList);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult Get(Guid id)
        {
            var entity = _empresaRepository.RetornarPorId(id);
            if (entity == null) return NotFound(new { message = "Item não encontrado" });

            var model = _mapper.Map<EmpresaModel>(_empresaRepository.RetornarPorId(id));
            return Ok(model);
        }

        [HttpPost]
        public IActionResult Post([FromBody]EmpresaModel model)
        {
            try
            {
                model.Id = Guid.NewGuid();

                var entity = Mapper.Map<Empresa>(model);

                #region Validações

                var errorsList = new List<object>();

                if (String.IsNullOrWhiteSpace(entity.RazaoSocial))
                    errorsList.Add(new { code = 34, message = "Campo é obrigatório", field = "Razão Social" });

                if (String.IsNullOrWhiteSpace(entity.SocioProprietario))
                    errorsList.Add(new { code = 34, message = "Campo é obrigatório", field = "Sócio Proprietário" });

                if (String.IsNullOrWhiteSpace(entity.Cnpj))
                    errorsList.Add(new { code = 34, message = "Campo é obrigatório", field = "CNPJ" });

                if (entity.Cnpj?.Length != 14)
                    errorsList.Add(new { code = 35, message = "Formato inválido", field = "CNPJ" });

                if (_empresaRepository.Listar(predicate: x => x.Cnpj == entity.Cnpj, metaonly: true).Count > 0)
                    errorsList.Add(new { code = 40, message = "Valor duplicado", field = "CNPJ" });

                #endregion

                if (errorsList.Count > 0)
                    return BadRequest(new { message = "Sua requisição possui erros de validação", errors = errorsList });

                var result = _empresaRepository.Cadastrar(entity);
                var locationUri = $"{Request.Scheme}://{Request.Host}{Request.Path}/{model.Id}";

                return Created(locationUri, new { message = "Item criado com sucesso" });
            }
            catch (Exception)
            {
                // TODO: logar exception

                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Ocorreu um erro interno no servidor. Favor contatar o suporte técnico." });
            }
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult Put(Guid id, [FromBody]EmpresaModel model)
        {
            try
            {
                model.Id = id;

                var entity = _empresaRepository.RetornarPorId(model.Id);
                if (entity == null) return NotFound(new { message = "Item não encontrado" });

                entity = Mapper.Map<Empresa>(model);

                #region Validações

                var errorsList = new List<object>();

                if (String.IsNullOrWhiteSpace(entity.RazaoSocial))
                    errorsList.Add(new { code = 34, message = "Campo é obrigatório", field = "Razão Social" });

                if (String.IsNullOrWhiteSpace(entity.SocioProprietario))
                    errorsList.Add(new { code = 34, message = "Campo é obrigatório", field = "Sócio Proprietário" });

                if (String.IsNullOrWhiteSpace(entity.Cnpj))
                    errorsList.Add(new { code = 34, message = "Campo é obrigatório", field = "CNPJ" });

                if (entity.Cnpj?.Length != 14)
                    errorsList.Add(new { code = 35, message = "Formato inválido", field = "CNPJ" });

                if (_empresaRepository.Listar(predicate: x => x.Cnpj == entity.Cnpj && x.Id != entity.Id, metaonly: true).Count > 0)
                    errorsList.Add(new { code = 40, message = "Valor duplicado", field = "CNPJ" });

                #endregion

                if (errorsList.Count > 0)
                    return BadRequest(new { message = "Sua requisição possui erros de validação", errors = errorsList });

                _empresaRepository.Atualizar(entity);

                return Ok(new { message = "Item atualizado com sucesso" });
            }
            catch (Exception)
            {
                // TODO: logar exception

                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Ocorreu um erro interno no servidor. Favor contatar o suporte técnico." });
            }
        }

        // TODO: ver como funciona esse método PATCH
        [HttpPut]
        public IActionResult Patch()
        {
            return Ok();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var entity = _empresaRepository.RetornarPorId(id);
                if (entity == null) return NotFound(new { message = "Item não encontrado" });

                _empresaRepository.Remover(id);

                return Ok(new { message = "Item removido com sucesso" });
            }
            catch (Exception)
            {
                // TODO: logar exception

                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Ocorreu um erro interno no servidor. Favor contatar o suporte técnico." });
            }
        }
    }
}