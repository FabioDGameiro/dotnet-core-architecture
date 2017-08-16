using Microsoft.AspNetCore.Mvc;

namespace RestfulAPI.Controllers.Base
{
    [Produces("application/json")]
    public abstract class BaseController : Controller
    {
    }
}