using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Domain.Base
{
    public sealed class AtivosSpecifications<T> : Specification<T> where T : Entity
    {
        public override Expression<Func<T, bool>> ToExpression()
        {
            return Entity => Entity.DataExclusao == null;
        }
    }

    public sealed class InativosSpecifications<T> : Specification<T> where T : Entity
    {
        public override Expression<Func<T, bool>> ToExpression()
        {
            return Entity => Entity.DataExclusao != null;
        }
    }
}
