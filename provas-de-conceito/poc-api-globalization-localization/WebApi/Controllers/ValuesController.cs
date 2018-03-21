using System;
using System.Globalization;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IStringLocalizer _localizer;

        public ValuesController(IStringLocalizerFactory factory)
        {
            var type = typeof(MessagesResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create("MessagesResource", assemblyName.Name);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = new
            {
                CurrentCulture = CultureInfo.CurrentCulture.NativeName,
                CurrentUICulture = CultureInfo.CurrentUICulture.NativeName,
                Date = DateTime.Now.ToString("D"),
                Number = (1234567.89).ToString("n"),
                Currency = (4452.24).ToString("C"),
                BundaLocalized = _localizer["Hello"].Value
            };

            return Ok(result);
        }
    }
}
