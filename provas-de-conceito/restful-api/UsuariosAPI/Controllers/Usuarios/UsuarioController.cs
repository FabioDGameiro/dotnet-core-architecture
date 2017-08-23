using AutoMapper;
using Domain.Usuarios;
using Domain.Usuarios.Parameters;
using Domain.Usuarios.Repository;
using Microsoft.AspNetCore.Http;
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
            if(_repository.UsuarioExists(usuarioId))
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