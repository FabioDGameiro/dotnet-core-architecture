using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace Project.MvcApp.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            if (!Url.IsLocalUrl(returnUrl)) returnUrl = "/";

            var props = new AuthenticationProperties
            {
                RedirectUri = returnUrl
            };

            return Challenge(props, "oidc");
        }

        public async Task<IActionResult> CallApiUsingUserAccessToken()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var client = new HttpClient();
            client.SetBearerToken(accessToken);
            var content = await client.GetStringAsync("http://localhost:3000/identity");

            ViewBag.Json = JArray.Parse(content).ToString();
            return View();
        }

        public IActionResult Denied(string returnUrl = null)
        {
            return View();
        }

        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }
    }
}