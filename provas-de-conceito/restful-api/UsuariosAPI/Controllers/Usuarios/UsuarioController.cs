using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsuariosAPI.Controllers.Base;
using Domain.Usuarios.Repository;
using Domain.Usuarios.Parameters;
using AutoMapper;
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
            // 1. Retorna usuarios do repositório
            var usuarios = _repository.Listar(parametros);

            // 2. Mapeia para a model com os dados formatados e retorna 200 - OK
            var usuariosModels = _mapper.Map<IEnumerable<UsuarioGetModel>>(usuarios);
            return Ok(usuariosModels);
        }

        // GET BY ID

        [HttpGet("{usuarioId:guid}")]
        public IActionResult Get(Guid usuarioId)
        {
            // 1. Retorna usuario do repositório
            var usuario = _repository.RetornarPorId(usuarioId);

            // 2. Checa se o recurso existe (retorna 404 - NOT FOUND se não existir)
            if (usuario == null) return NotFound();

            // 3. Mapeia para a model com os dados formatados e retorna 200 - OK
            var usuarioModel = _mapper.Map<UsuarioGetModel>(usuario);
            return Ok(usuarioModel);
        }

        // POST

        // PUT

        // DELETE

        // OPTIONS
    }
}