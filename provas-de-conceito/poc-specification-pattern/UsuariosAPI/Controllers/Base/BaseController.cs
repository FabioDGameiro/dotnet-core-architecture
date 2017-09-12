#region Using

using Microsoft.AspNetCore.Mvc;

#endregion

namespace UsuariosAPI.Controllers.Base
{
    [Produces("application/json")]
    public abstract class BaseController : Controller
    {
    }
}