using System;
using System.Linq.Expressions;
using LinqSpecs;

namespace poc_specification_pattern.Users.Specifications
{
    public class UsersFromGender : Specification<User>
    {
        private readonly GenderType _gender;

        public UsersFromGender(GenderType gender)
        {
            _gender = gender;
        }

        public override Expression<Func<User, bool>> ToExpression()
        {
            return user => user.Gender == _gender;
        }
    }
}