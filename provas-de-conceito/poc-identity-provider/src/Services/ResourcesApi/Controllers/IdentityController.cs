using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ResourcesApi.Controllers
{
    [Authorize]
    [Route("identity")]
    public class IdentityController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}