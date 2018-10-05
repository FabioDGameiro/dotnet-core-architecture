using poc_specification_pattern.Users;
using LinqSpecs;
using System;
using System.Linq.Expressions;

namespace poc_specification_pattern.Users.Specifications
{
    public class UsersFromCountry : Specification<User>
    {
        private readonly int _countryId;

        public UsersFromCountry(int countryId)
        {
            _countryId = countryId;
        }

        public override Expression<Func<User, bool>> ToExpression()
        {
            return user => user.CountryId == _countryId;
        }
    }
}