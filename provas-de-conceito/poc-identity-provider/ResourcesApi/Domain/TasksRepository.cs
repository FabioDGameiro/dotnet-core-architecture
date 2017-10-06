using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm.Expressions;

namespace ResourcesApi.Domain
{
    public class TasksRepository
    {
        private readonly List<Task> _taskList;

        public TasksRepository()
        {
            _taskList = new List<Task>
            {
                new Task
                {
                    Id = new Guid("4da11638-97b7-4779-92c1-f05dbc1d09fc"),
                    UserId = new Guid("b07761f1-2a3e-42d8-9051-707ce2c98dbc"),
                    Description = "Tarefa Teste do Tiago",
                    IsFinished = false
                },
                new Task
                {
                    Id = new Guid("b32d7001-ad89-4858-a9f3-c71b2b96a20d"),
                    UserId = new Guid("aab263d7-b2ac-43d5-bbc2-fc812ce658d3"),
                    Description = "Tarefa Teste da Mércia",
                    IsFinished = false
                },
                new Task
                {
                    Id = new Guid("826023b6-c4db-424f-ab04-29370d026e58"),
                    UserId = new Guid("56639436-0465-471f-ba19-54259a399cd5"),
                    Description = "Tarefa Teste do Iran",
                    IsFinished = true
                },
            };
        }

        public void Add(Task task)
        {
            _taskList.Add(task);
        }

        public void Update(Task task)
        {
            var index = _taskList.FindIndex(x => x.Id == task.Id);
            _taskList[index] = task;
        }

        public Task GetById(Guid id)
        {
            return _taskList.FirstOrDefault(x => x.Id == id);
        }

        public void Delete(Guid id)
        {
            _taskList.Remove(GetById(id));
        }

        public IEnumerable<Task> List(Guid userId)
        {
            return _taskList.Where(x => x.UserId == userId);
        }
    }
}
