using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Base
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public DateTime? DataExclusao { get; set; }
    }
}
