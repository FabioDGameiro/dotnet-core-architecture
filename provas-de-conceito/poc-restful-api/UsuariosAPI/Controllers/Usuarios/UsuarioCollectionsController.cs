#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Domain.Usuarios;
using Domain.Usuarios.Repository;
using Infra.Helpers;
using Microsoft.AspNetCore.Mvc;
using UsuariosAPI.Controllers.Base;
using UsuariosAPI.Models.Usuarios;

#endregion

namespace UsuariosAPI.Controllers.Usuarios
{
    [Route("api/usuarios-collections")]
    public class UsuarioCollectionsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _repository;

        public UsuarioCollectionsController(IUsuarioRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // POST

        [HttpPost]
        public IActionResult Create([FromBody] IEnumerable<CreateUsuarioModel> usuariosCollections)
        {
            if (usuariosCollections == null) return BadRequest();

            for (var i = 0; i < usuariosCollections.Count(); i++)
                // Valida email duplicado
                if (_repository.EmailExists(usuariosCollections.ElementAt(i).Email))
                    ModelState.AddModelError($"[{i}].Email", "O e-mail informado já está sendo utilizado");

            if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);

            var usuariosEntities = _mapper.Map<IEnumerable<Usuario>>(usuariosCollections);

            foreach (var usuario in usuariosEntities)
                _repository.CadastrarUsuario(usuario);

            if (!_repository.Save())
                throw new Exception("Ocorreu um erro inesperado ao salvar usuário");

            var usuariosModels = _mapper.Map<IEnumerable<GetUsuarioModel>>(usuariosEntities);

            var createdIds = string.Join(",", usuariosModels.Select(x => x.Id));
            var locationUri = $"{Request.Scheme}://{Request.Host}{Request.Path}/({createdIds})";

            return Created(locationUri, usuariosModels);
        }

        // GET BY IDs

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