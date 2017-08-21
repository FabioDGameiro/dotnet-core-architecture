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
            var usuarios = _repository.Listar(parametros);
            var usuariosModels = _mapper.Map<IEnumerable<UsuarioGetModel>>(usuarios);

            return Ok(usuariosModels);
        }

        // GET BY ID

        // POST

        // PUT

        // DELETE

        // OPTIONS
    }
}