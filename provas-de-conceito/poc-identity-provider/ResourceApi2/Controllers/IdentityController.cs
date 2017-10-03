using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ResourceApi2.Controllers
{
    [Authorize]
    [Route("identity")]
    public class IdentityController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            //return Ok(new[] { new { Nome = "banana" } });
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}
