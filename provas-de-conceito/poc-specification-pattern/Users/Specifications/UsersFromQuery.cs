using System;
using System.Linq.Expressions;
using LinqSpecs;

namespace poc_specification_pattern.Users.Specifications
{
    public class UsersFromQuery : Specification<User>
    {
        private readonly string _query;

        public UsersFromQuery(string query)
        {
            _query = query;
        }

        public override Expression<Func<User, bool>> ToExpression()
        {
            return user => 
                user.Email.Contains(_query) ||
                user.Name.Contains(_query);
        }
    }
}