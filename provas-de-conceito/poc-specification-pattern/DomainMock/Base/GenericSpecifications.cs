#region Using

using System;
using System.Linq.Expressions;

#endregion

namespace Domain.Base
{
    public sealed class AtivosSpecifications<T> : Specification<T> where T : Entity
    {
        public override Expression<Func<T, bool>> ToExpression()
        {
            return entity => entity.DataExclusao == null;
        }
    }

    public sealed class InativosSpecifications<T> : Specification<T> where T : Entity
    {
        public override Expression<Func<T, bool>> ToExpression()
        {
            return entity => entity.DataExclusao != null;
        }
    }
}