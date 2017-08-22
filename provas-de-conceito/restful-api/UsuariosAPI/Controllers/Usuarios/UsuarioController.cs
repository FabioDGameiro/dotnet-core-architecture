using AutoMapper;
using Domain.Usuarios;
using Domain.Usuarios.Parameters;
using Domain.Usuarios.Repository;
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

        // GET ALL

        [HttpGet]
        public IActionResult Get(UsuarioParameters parametros)
        {
            // Retorna usuarios do repositório
            var usuarios = _repository.Listar(parametros);

            // Mapeia para a model com os dados formatados e retorna 200 - OK
            var usuariosModels = _mapper.Map<IEnumerable<GetUsuarioModel>>(usuarios);
            return Ok(usuariosModels);
        }

        // GET BY ID

        [HttpGet("{usuarioId:guid}")]
        public IActionResult Get(Guid usuarioId)
        {
            // Retorna usuario do repositório
            var usuario = _repository.RetornarPorId(usuarioId);

            // Checa se o recurso existe (retorna 404 - NOT FOUND se não existir)
            if (usuario == null) return NotFound();

            // Mapeia para a model com os dados formatados e retorna 200 - OK
            var usuarioModel = _mapper.Map<GetUsuarioModel>(usuario);
            return Ok(usuarioModel);
        }

        // POST
        // TODO: criar um teste para cadastrar usuario com endereço

        [HttpPost]
        public IActionResult Post([FromBody] CreateUsuarioModel usuarioModel)
        {
            // Checa se o a model foi preenchida corretamente
            if (usuarioModel == null) return BadRequest();

            // Mapeia a model para a entidade
            var usuarioEntity = _mapper.Map<Usuario>(usuarioModel);

            // Adiciona ao repositório
            _repository.Cadastrar(usuarioEntity);

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

        // PUT

        // DELETE

        // OPTIONS
    }
}