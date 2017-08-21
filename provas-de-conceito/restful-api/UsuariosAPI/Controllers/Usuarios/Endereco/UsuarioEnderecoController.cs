using AutoMapper;
using Domain.Usuarios.Parameters;
using Domain.Usuarios.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using UsuariosAPI.Controllers.Base;
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
            // 1. Checa se o usuário existe (retorna 404 - NOT FOUND se não existir)
            if (!_repository.UsuarioExists(usuarioId)) return NotFound();

            // 2. Retorna endereços de um usuário pelo repositório
            var enderecos = _repository.ListarEnderecosPorUsuario(usuarioId);

            // 3. Mapeia para a model com os dados formatados e retorna 200 - OK
            var enderecosModels = _mapper.Map<IEnumerable<UsuarioEnderecoGetModel>>(enderecos);
            return Ok(enderecosModels);
        }

        // GET BY ID

        [HttpGet("{enderecoId:guid}")]
        public IActionResult Get(Guid usuarioId, Guid enderecoId)
        {
            // 1. Checa se o usuário existe (retorna 404 - NOT FOUND se não existir)
            if (!_repository.UsuarioExists(usuarioId)) return NotFound();

            // 2. Retorna endereço do usuário pelo repositório
            var endereco = _repository.RetornarEnderecoPorId(usuarioId, enderecoId);

            // 3. Checa se o recurso existe (retorna 404 - NOT FOUND se não existir)
            if (endereco == null) return NotFound();

            // 4. Mapeia para a model com os dados formatados e retorna 200 - OK
            var enderecoModel = _mapper.Map<UsuarioEnderecoGetModel>(endereco);
            return Ok(enderecoModel);
        }

        // POST

        // PUT

        // DELETE

        // OPTIONS
    }
}