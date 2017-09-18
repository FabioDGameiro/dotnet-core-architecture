#region Using

using System;

#endregion

namespace Domain.Base
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public DateTime? DataExclusao { get; set; }
    }
}