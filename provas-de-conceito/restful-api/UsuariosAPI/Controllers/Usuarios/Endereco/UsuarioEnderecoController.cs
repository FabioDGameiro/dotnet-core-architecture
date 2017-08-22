using AutoMapper;
using Domain.Usuarios.Endereco;
using Domain.Usuarios.Parameters;
using Domain.Usuarios.Repository;
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
        public IActionResult Get(Guid usuarioId, UsuarioEnderecoParameters parametros)
        {
            // Checa se o usuário existe (retorna 404 - NOT FOUND se não existir)
            if (!_repository.UsuarioExists(usuarioId)) return NotFound();

            // Retorna endereços de um usuário pelo repositório
            var enderecos = _repository.ListarEnderecosPorUsuario(usuarioId);

            // Mapeia para a model com os dados formatados e retorna 200 - OK
            var enderecosModels = _mapper.Map<IEnumerable<GetUsuarioEnderecoModel>>(enderecos);
            return Ok(enderecosModels);
        }

        // GET BY ID

        [HttpGet("{enderecoId:guid}")]
        public IActionResult Get(Guid usuarioId, Guid enderecoId)
        {
            // Checa se o usuário existe (retorna 404 - NOT FOUND se não existir)
            if (!_repository.UsuarioExists(usuarioId)) return NotFound();

            // Retorna endereço do usuário pelo repositório
            var endereco = _repository.RetornarEnderecoPorId(usuarioId, enderecoId);

            // Checa se o recurso existe (retorna 404 - NOT FOUND se não existir)
            if (endereco == null) return NotFound();

            // Mapeia para a model com os dados formatados e retorna 200 - OK
            var enderecoModel = _mapper.Map<GetUsuarioEnderecoModel>(endereco);
            return Ok(enderecoModel);
        }

        // POST

        [HttpPost]
        public IActionResult Post(Guid usuarioId, [FromBody] CreateUsuarioEnderecoModel usuarioEnderecoModel)
        {
            // Checa se o a model foi preenchida corretamente
            if (usuarioEnderecoModel == null) return BadRequest();

            // Checa se o usuário existe (retorna 404 - NOT FOUND se não existir)
            if (!_repository.UsuarioExists(usuarioId)) return NotFound();

            // Mapeia a model para a entidade
            var usuarioEnderecoEntity = _mapper.Map<UsuarioEndereco>(usuarioEnderecoModel);

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

        // PUT

        // DELETE

        // OPTIONS
    }
}