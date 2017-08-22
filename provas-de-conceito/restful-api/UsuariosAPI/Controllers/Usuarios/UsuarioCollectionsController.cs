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
                _repository.Cadastrar(usuario);
            }

            if (!_repository.Save())
            {
                throw new Exception("Ocorreu um erro inesperado ao salvar usuário");
            }

            // TODO: trazer os locations das coleções
            //var locationUri = $"{Request.Scheme}://{Request.Host}{Request.Path}/{usuarioEntity.Id}";

            return Ok();
        }
    }
}