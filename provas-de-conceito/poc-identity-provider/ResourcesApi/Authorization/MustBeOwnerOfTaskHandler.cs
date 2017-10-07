using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using ResourcesApi.Domain;

namespace ResourcesApi.Authorization
{
    public class MustBeOwnerOfTaskHandler : AuthorizationHandler<MustBeOwnerOfTask>
    {
        public static readonly TasksRepository TaskRepository = new TasksRepository();

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, MustBeOwnerOfTask requirement)
        {
            var filterContext = context.Resource as AuthorizationFilterContext;
            if (filterContext == null)
            {
                context.Fail();
                return Task.FromResult(0);
            }

            var taskId = Guid.Parse(filterContext.RouteData.Values["id"].ToString());
            var userId = Guid.Parse(context.User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value);

            if (!TaskRepository.IsOwnerOfTask(taskId, userId))
            {
                context.Fail();
                return Task.FromResult(0);
            }

            context.Succeed(requirement);
            return Task.FromResult(0);
        }
    }
}
