using AutoMapper;
using Domain.Usuarios.Endereco;
using Domain.Usuarios.Parameters;
using Domain.Usuarios.Repository;
using Infra.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using UsuariosAPI.Controllers.Base;
using UsuariosAPI.Models.Usuarios;
using UsuariosAPI.Models.Usuarios.Endereco;

namespace UsuariosAPI.Controllers.Usuarios.Enderecos
{
    [Route("api/usuarios/{usuarioId:guid}/enderecos")]
    public class UsuarioEnderecoController : BaseController
    {
        public readonly IUsuarioRepository _repository;
        public readonly IMapper _mapper;

        public UsuarioEnderecoController(IUsuarioRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET ALL

        [HttpGet]
        public IActionResult Get(UsuarioEnderecoParameters parametros)
        {
            // Checa se o usuário existe (retorna 404 - NOT FOUND se não existir)
            if (!_repository.UsuarioExists(parametros.UsuarioId)) return NotFound();

            // Retorna endereços de um usuário pelo repositório
            var enderecosPagedList = _repository.ListarEnderecosPorUsuario(parametros);

            // Gera os metadados da paginação e adiciona ao cabeçalho

            var paginationMetadata = new
            {
                totalCount = enderecosPagedList.TotalCount,
                pageSize = enderecosPagedList.PageSize,
                currentPage = enderecosPagedList.CurrentPage,
                totalPages = enderecosPagedList.TotalPages
            };

            Response.Headers.Add("Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));

            // Mapeia para a model com os dados formatados e retorna 200 - OK
            var enderecosModels = _mapper.Map<IEnumerable<GetUsuarioEnderecoModel>>(enderecosPagedList);
            return Ok(enderecosModels);
        }

        // GET BY ID

        [HttpGet("{enderecoId:guid}")]
        public IActionResult Get(Guid usuarioId, Guid enderecoId)
        {
            // Checa se o usuário existe (retorna 404 - NOT FOUND se não existir)
            if (!_repository.UsuarioExists(usuarioId)) return NotFound();

            // Retorna endereço do usuário pelo repositório
            var endereco = _repository.RetornarEndereco(usuarioId, enderecoId);

            // Checa se o recurso existe (retorna 404 - NOT FOUND se não existir)
            if (endereco == null) return NotFound();

            // Mapeia para a model com os dados formatados e retorna 200 - OK
            var enderecoModel = _mapper.Map<GetUsuarioEnderecoModel>(endereco);
            return Ok(enderecoModel);
        }

        // POST

        [HttpPost]
        public IActionResult Post(Guid usuarioId, [FromBody] CreateUsuarioEnderecoModel enderecoModel)
        {
            // Checa se o a model foi preenchida corretamente
            if (enderecoModel == null) return BadRequest();

            // Checa se a model está inválida, (retorna 422 - UnprocessableEntity se inválida)
            if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);

            // Checa se o usuário existe (retorna 404 - NOT FOUND se não existir)
            if (!_repository.UsuarioExists(usuarioId)) return NotFound();

            // Mapeia a model para a entidade
            var usuarioEnderecoEntity = _mapper.Map<UsuarioEndereco>(enderecoModel);

            // Adiciona ao repositório
            _repository.CadastrarEnderecoPorUsuario(usuarioId, usuarioEnderecoEntity);

            // Persiste os dados no banco de dados
            if (!_repository.Save())
            {
                // Joga uma exceção se der algum erro ao salvar
                throw new Exception("Ocorreu um erro inesperado ao salvar endereço do usuário");
            }

            // Cria a URI do local de onde o recurso foi criado
            var locationUri = $"{Request.Scheme}://{Request.Host}{Request.Path}/{usuarioEnderecoEntity.Id}";

            // Mapeia a entidade criada para a model de retorno para a confirmação de criação
            var enderecoCriadoModel = _mapper.Map<GetUsuarioEnderecoModel>(usuarioEnderecoEntity);

            // Retorna um Status Code 201 - Created At com o recurso criado
            return Created(locationUri, enderecoCriadoModel);
        }

        // POST BY ID

        [HttpPost("{enderecoId:guid}")]
        public IActionResult Post(Guid usuarioId, Guid enderecoId)
        {
            if (_repository.EnderecoExists(usuarioId, enderecoId))
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }

            return NotFound();
        }

        // PUT

        [HttpPut("{enderecoId:guid}")]
        public IActionResult Put(Guid usuarioId, Guid enderecoId, [FromBody] UpdateUsuarioEnderecoModel enderecoModel)
        {
            if (enderecoModel == null) return BadRequest();

            if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);

            if (!_repository.UsuarioExists(usuarioId)) return NotFound();

            var enderecoEntity = _repository.RetornarEndereco(usuarioId, enderecoId);
            if (enderecoEntity == null) return NotFound();

            _mapper.Map(enderecoModel, enderecoEntity);

            _repository.AtualizaUsuarioEndereco(enderecoEntity);

            if (!_repository.Save())
            {
                throw new Exception("Ocorreu um erro inesperado ao atualizar endereço do usuário");
            }

            return NoContent();
        }

        // PATCH

        [HttpPatch("{enderecoId:guid}")]
        public IActionResult Patch(Guid usuarioId, Guid enderecoId, [FromBody] JsonPatchDocument<UpdateUsuarioEnderecoModel> patchEnderecoModel)
        {
            if (patchEnderecoModel == null) return BadRequest();

            if (!_repository.UsuarioExists(usuarioId)) return NotFound();

            var enderecoEntity = _repository.RetornarEndereco(usuarioId, enderecoId);
            if (enderecoEntity == null) return NotFound();

            // mapeia entidade para uma model que será atualizada
            var enderecoToPatch = _mapper.Map<UpdateUsuarioEnderecoModel>(enderecoEntity);

            patchEnderecoModel.ApplyTo(enderecoToPatch, ModelState);

            // tenta revalidar a model, depois de aplicar as novas alterações
            TryValidateModel(enderecoToPatch);

            // checa se a model está inválida, (retorna 422 - UnprocessableEntity se inválida)
            if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);

            // atualiza a entidade com a model atualizada 
            _mapper.Map(enderecoToPatch, enderecoEntity);

            // atualiza entidade no repositório
            _repository.AtualizaUsuarioEndereco(enderecoEntity);

            if (!_repository.Save())
            {
                throw new Exception("Ocorreu um erro inesperado ao atualizar endereço do usuário");
            }

            return NoContent();
        }

        // DELETE

        [HttpDelete("{enderecoId:guid}")]
        public IActionResult Delete(Guid usuarioId, Guid enderecoId)
        {
            // Checa se o usuário existe (retorna 404 - NOT FOUND se não existir)
            if (!_repository.UsuarioExists(usuarioId)) return NotFound();

            // Retorna endereço do usuário pelo repositório
            var endereco = _repository.RetornarEndereco(usuarioId, enderecoId);

            // Checa se o recurso existe (retorna 404 - NOT FOUND se não existir)
            if (endereco == null) return NotFound();

            // Remove entidade do repositorio
            _repository.RemoveEndereco(endereco);

            // Persiste os dados no banco de dados
            if (!_repository.Save())
            {
                // Joga uma exceção se der algum erro ao salvar
                throw new Exception("Ocorreu um erro inesperado ao salvar endereço do usuário");
            }

            return NoContent();
        }

        // OPTIONS
    }
}