using poc_specification_pattern.Users;
using System;
using System.Linq.Expressions;
using LinqSpecs;

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