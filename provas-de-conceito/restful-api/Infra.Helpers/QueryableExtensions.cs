using Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Infra.Helpers
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string sortCollection) where T : Entity
        {
            if (String.IsNullOrWhiteSpace(sortCollection))
                return source;

            var expression = source.Expression;
            var count = 0;

            foreach (var sort in sortCollection.Split(','))
            {
                var isDesc = sort.Contains("-desc");
                var property = sort.Trim().Replace("-desc", string.Empty);

                var parameter = Expression.Parameter(typeof(T), "x");

                if (!parameter.Type.HasProperty(property))
                    continue;

                var member = Expression.Property(parameter, property);

                var selector = Expression.PropertyOrField(parameter, property);
                var method = isDesc ?
                    (count == 0 ? "OrderByDescending" : "ThenByDescending") :
                    (count == 0 ? "OrderBy" : "ThenBy");
                expression = Expression.Call(typeof(Queryable), method, new[] { source.ElementType, selector.Type }, expression, Expression.Quote(Expression.Lambda(selector, parameter)));
                count++;
            }

            return count > 0 ? source.Provider.CreateQuery<T>(expression) : source;
        }

        public static bool HasProperty(this Type type, string propertyName)
        {
            return type.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) != null;
        }
    }

    

}

