using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MvcClient.Controllers
{
    public class PayAreaController : Controller
    {
        [Authorize(Roles = "Subscriber, Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}