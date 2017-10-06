using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResourcesApi.Domain;

namespace ResourcesApi.Controllers
{
    [Authorize]
    [Route("tasks")]
    public class TaskController : Controller
    {
        public static readonly TasksRepository TaskRepository = new TasksRepository();

        [HttpGet]
        public IActionResult Get()
        {
            var userId = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value);
            return Ok(TaskRepository.List(userId));
        }
    }
}