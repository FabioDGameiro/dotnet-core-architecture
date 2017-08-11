using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestfulAPI.Controllers.Base;
using RestfulAPI.Models.Empresa;

namespace RestfulAPI.Controllers.Empresas
{
    [Route("api/empresas")]
    public class EmpresaController : BaseController
    {
        [HttpGet]
        [Route("")]
        public IActionResult Get(EmpresaFilterModel filter)
        {
            return Ok();
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult Get(Guid id)
        {
            return Ok();
        }

        [HttpPost]
        [Route("")]
        public IActionResult Post(EmpresaModel model)
        {
            return Ok();
        }

        [HttpPut]
        [Route("")]
        public IActionResult Put(EmpresaModel model)
        {
            return Ok();
        }

        // TODO: ver como funciona esse método PATCH
        [HttpPut]
        [Route("")]
        public IActionResult Patch()
        {
            return Ok();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            return Ok();
        }
    }
}