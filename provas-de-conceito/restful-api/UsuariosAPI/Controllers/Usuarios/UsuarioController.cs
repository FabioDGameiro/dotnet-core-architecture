﻿using AutoMapper;
using Domain.Usuarios;
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

namespace UsuariosAPI.Controllers.Usuarios
{
    [Route("api/usuarios")]
    public class UsuarioController : BaseController
    {
        public readonly IUsuarioRepository _repository;
        public readonly IMapper _mapper;

        public UsuarioController(IUsuarioRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET

        [HttpGet]
        public IActionResult Get(UsuarioParameters parametros)
        {
            // Retorna usuarios do repositório
            var usuarios = _repository.RetornaUsuarios(parametros);

            // Mapeia para a model com os dados formatados e retorna 200 - OK
            var usuariosModels = _mapper.Map<IEnumerable<GetUsuarioModel>>(usuarios);
            return Ok(usuariosModels);
        }

        // GET BY ID

        [HttpGet("{usuarioId:guid}")]
        public IActionResult Get(Guid usuarioId)
        {
            // Retorna usuario do repositório
            var usuario = _repository.RetornaUsuario(usuarioId);

            // Checa se o recurso existe (retorna 404 - NOT FOUND se não existir)
            if (usuario == null) return NotFound();

            // Mapeia para a model com os dados formatados e retorna 200 - OK
            var usuarioModel = _mapper.Map<GetUsuarioModel>(usuario);
            return Ok(usuarioModel);
        }

        // POST

        [HttpPost]
        public IActionResult Post([FromBody] CreateUsuarioModel usuarioModel)
        {
            // Checa se o a model foi preenchida corretamente
            if (usuarioModel == null) return BadRequest();

            // Valida email duplicado
            if (_repository.EmailExists(usuarioModel.Email))
                ModelState.AddModelError("Email", "O e-mail informado já está sendo utilizado");

            // checa se a model está inválida, (retorna 422 - UnprocessableEntity se inválida)
            if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);

            // Mapeia a model para a entidade
            var usuarioEntity = _mapper.Map<Usuario>(usuarioModel);

            // Adiciona ao repositório
            _repository.CadastrarUsuario(usuarioEntity);

            // Persiste os dados no banco de dados
            if (!_repository.Save())
            {
                // Joga uma exceção se der algum erro ao salvar
                throw new Exception("Ocorreu um erro inesperado ao salvar usuário");
            }

            // Cria a URI do local de onde o recurso foi criado
            var locationUri = $"{Request.Scheme}://{Request.Host}{Request.Path}/{usuarioEntity.Id}";

            // Mapeia a entidade criada para a model de retorno para a confirmação de criação
            var usuarioCriadoModel = _mapper.Map<GetUsuarioModel>(usuarioEntity);

            // Retorna um Status Code 201 - Created At com o recurso criado
            return Created(locationUri, usuarioCriadoModel);
        }

        // POST BY ID

        [HttpPost("{usuarioId:guid}")]
        public IActionResult Post(Guid usuarioId)
        {
            if (_repository.UsuarioExists(usuarioId))
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }

            return NotFound();
        }

        // PUT

        [HttpPut("{usuarioId:guid}")]
        public IActionResult Put(Guid usuarioId, [FromBody] UpdateUsuarioModel usuarioModel)
        {
            if (usuarioModel == null) return BadRequest();

            // Valida email duplicado
            if (_repository.EmailExists(usuarioModel.Email, usuarioId))
                ModelState.AddModelError("Email", "O e-mail informado já está sendo utilizado");

            if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);

            if (!_repository.UsuarioExists(usuarioId)) return NotFound();

            var usuarioEntity = _repository.RetornaUsuario(usuarioId);
            if (usuarioEntity == null) return NotFound();

            _mapper.Map(usuarioModel, usuarioEntity);

            _repository.AtualizaUsuario(usuarioEntity);

            if (!_repository.Save())
            {
                throw new Exception("Ocorreu um erro inesperado ao atualizar o usuário");
            }

            return NoContent();
        }

        // PATCH

        [HttpPatch("{usuarioId:guid}")]
        public IActionResult Patch(Guid usuarioId, [FromBody] JsonPatchDocument<UpdateUsuarioModel> patchUsuarioModel)
        {
            if (patchUsuarioModel == null) return BadRequest();

            var usuarioEntity = _repository.RetornaUsuario(usuarioId);
            if (usuarioEntity == null) return NotFound();

            // mapeia entidade para uma model que será atualizada
            var usuarioToPatch = _mapper.Map<UpdateUsuarioModel>(usuarioEntity);

            patchUsuarioModel.ApplyTo(usuarioToPatch, ModelState);

            // Valida email duplicado
            if (_repository.EmailExists(usuarioToPatch.Email, usuarioId))
                ModelState.AddModelError("Email", "O e-mail informado já está sendo utilizado");

            // tenta revalidar a model, depois de aplicar as novas alterações
            TryValidateModel(usuarioToPatch);

            // checa se a model está inválida, (retorna 422 - UnprocessableEntity se inválida)
            if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);

            // atualiza a entidade com a model atualizada 
            _mapper.Map(usuarioToPatch, usuarioEntity);

            // atualiza entidade no repositório
            _repository.AtualizaUsuario(usuarioEntity);

            if (!_repository.Save())
            {
                throw new Exception("Ocorreu um erro inesperado ao atualizar o usuário");
            }

            return NoContent();
        }

        // DELETE

        [HttpDelete("{usuarioId:guid}")]
        public IActionResult Delete(Guid usuarioId)
        {
            // Checa se o usuário existe (retorna 404 - NOT FOUND se não existir)
            if (!_repository.UsuarioExists(usuarioId)) return NotFound();

            // Retorna usuario pelo repositório
            var usuario = _repository.RetornaUsuario(usuarioId);

            // Checa se o recurso existe (retorna 404 - NOT FOUND se não existir)
            if (usuario == null) return NotFound();

            // Remove entidade do repositorio
            _repository.RemoveUsuario(usuario);

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