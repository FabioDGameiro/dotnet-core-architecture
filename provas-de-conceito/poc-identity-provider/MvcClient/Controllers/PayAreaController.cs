using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MvcClient.Controllers
{
    public class PayAreaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}