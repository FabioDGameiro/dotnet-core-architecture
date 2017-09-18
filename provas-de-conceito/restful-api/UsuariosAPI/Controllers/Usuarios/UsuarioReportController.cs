using Infra.CrossCutting.Reports.UsuariosReports.Repository;
using Microsoft.AspNetCore.Mvc;
using UsuariosAPI.Controllers.Base;

namespace UsuariosAPI.Controllers.Usuarios
{
    [Route("api/usuarios-reports")]
    public class UsuarioReportController : BaseController
    {
        public readonly IUsuarioReportService _service;

        public UsuarioReportController(IUsuarioReportService service)
        {
            _service = service;
        }

        [HttpGet("usuarios-com-endereco")]
        public IActionResult GetUsuariosComEndereco()
        {
            var usuariosComEnderecoModel = _service.RetornaUsuariosComEndereco();
            return Ok(usuariosComEnderecoModel);
        }

        [HttpGet("usuarios-sem-endereco")]
        public IActionResult GetUsuariosSemEndereco()
        {
            var usuariosComEnderecoModel = _service.RetornaUsuariosSemEndereco();
            return Ok(usuariosComEnderecoModel);
        }
    }
}