using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcClient.Models
{
    public class TaskViewModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Description { get; set; }
        public bool IsFinished { get; set; }
    }
}
