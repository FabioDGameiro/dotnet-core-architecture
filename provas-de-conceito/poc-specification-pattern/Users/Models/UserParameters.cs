using System;
using System.Linq.Expressions;
using LinqSpecs;
using poc_specification_pattern.Users.Specifications;

namespace poc_specification_pattern.Users.Models
{
    public class UserParameters
    {
        public int? CountryId { get; set; }
        public GenderType? Gender { get; set; }
        public string Query { get; set; }

        public Specification<User> ToSpecification()
        {
            Specification<User> spec = new TrueSpecification<User>();

            if (CountryId.HasValue)
            {
                spec &= new UsersFromCountry(CountryId.Value);
            }

            if (Gender.HasValue)
            {
                spec &= new UsersFromGender(Gender.Value);
            }

            if (!string.IsNullOrWhiteSpace(Query))
            {
                spec &= new UsersFromQuery(Query);
            }

            return spec;
        }
    }
}