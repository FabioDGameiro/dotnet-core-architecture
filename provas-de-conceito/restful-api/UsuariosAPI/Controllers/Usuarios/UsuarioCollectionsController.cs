using AutoMapper;
using Domain.Usuarios;
using Domain.Usuarios.Parameters;
using Domain.Usuarios.Repository;
using Library.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using UsuariosAPI.Controllers.Base;
using UsuariosAPI.Models.Usuarios;


namespace UsuariosAPI.Controllers.Usuarios
{
    [Route("api/usuarios-collections")]
    public class UsuarioCollectionsController : BaseController
    {
        public readonly IUsuarioRepository _repository;
        public readonly IMapper _mapper;

        public UsuarioCollectionsController(IUsuarioRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Create([FromBody] IEnumerable<CreateUsuarioModel> usuariosCollections)
        {
            if (usuariosCollections == null) return BadRequest();

            var usuariosEntities = _mapper.Map<IEnumerable<Usuario>>(usuariosCollections);

            foreach (var usuario in usuariosEntities)
            {
                _repository.CadastrarUsuario(usuario);
            }

            if (!_repository.Save())
            {
                throw new Exception("Ocorreu um erro inesperado ao salvar usuário");
            }

            var usuariosModels = _mapper.Map<IEnumerable<GetUsuarioModel>>(usuariosEntities);

            var createdIds = string.Join(",", usuariosModels.Select(x => x.Id));
            var locationUri = $"{Request.Scheme}://{Request.Host}{Request.Path}/({createdIds})";

            return Created(locationUri, usuariosModels);
        }

        [HttpGet("({ids})")]
        public IActionResult Get([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null) return BadRequest();

            var usuariosEntities = _repository.RetornaUsuarios(ids);

            if (ids.Count() != usuariosEntities.Count()) return NotFound();

            var usuariosModels = _mapper.Map<IEnumerable<GetUsuarioModel>>(usuariosEntities);

            return Ok(usuariosModels);
        }
    }
}