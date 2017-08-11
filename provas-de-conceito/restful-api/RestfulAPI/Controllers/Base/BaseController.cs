using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RestfulAPI.Controllers.Base
{
    [Produces("application/json")]
    public abstract class BaseController : Controller
    {
    }
}