using System;
using System.Linq.Expressions;
using LinqSpecs;

namespace poc_specification_pattern.Shared.Specifications
{
    public class ActiveRegisters<T> : Specification<T>
        where T : Entity
    {
        public override Expression<Func<T, bool>> ToExpression()
        {
            return entity => entity.IsRemoved == false;
        }
    }
}