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

        [HttpPost("finish/{id:guid}")]
        [Authorize("MustBeOwnerOfTask")]
        public IActionResult Put(Guid id)
        {
            // O código abaixo não é mais necessário pois está implementado na Policy "MustBeOwnerOfTask"

            //var userId = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value);

            //if (!TaskRepository.IsOwnerOfTask(id, userId))
            //    return Forbid();

            var updatedTask = TaskRepository.GetById(id);
            updatedTask.IsFinished = true;

            TaskRepository.Update(updatedTask);

            return Ok();
        }
    }
}