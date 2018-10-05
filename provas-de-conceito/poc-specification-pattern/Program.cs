using System;
using System.Collections.Generic;
using System.Linq;
using LinqSpecs;
using poc_specification_pattern.Users;
using poc_specification_pattern.Users.Models;
using poc_specification_pattern.Users.Specifications;

namespace poc_specification_pattern
{
    class Program
    {
        static void Main(string[] args)
        {
            // StaticSpecifications();
            DinamicSpecifications();
        }

        static void DinamicSpecifications()
        {
            var parameters = new UserParameters();

            parameters.CountryId = 1;
            parameters.Gender = GenderType.Male;

            var filteredUsers = FindUsers(parameters.ToSpecification());

            foreach (var user in filteredUsers)
            {
                Console.WriteLine(user);
            }
        }

        static void StaticSpecifications()
        {
            // Specifications
            var usersFromBrasil = new UsersFromCountry(2);
            var femaleUsers = new UsersFromGender(GenderType.Female);

            var filteredUsers = FindUsers(usersFromBrasil && femaleUsers);

            foreach (var user in filteredUsers)
            {
                Console.WriteLine(user);
            }
        }

        static IEnumerable<User> FindUsers(Specification<User> specifications) 
        { 
            var users = GetUsers();

            return users
                .Where(specifications.ToExpression().Compile())
                .ToList();
        }

        static IEnumerable<User> GetUsers()
        {
            var users = new List<User>
                {
                    new User
                    {
                    Id = 1,
                    Name = "User One",
                    CountryId = 1,
                    Gender = GenderType.Male
                    },
                    new User
                    {
                    Id = 2,
                    Name = "User Two",
                    CountryId = 1,
                    Gender = GenderType.Male
                    },
                    new User
                    {
                    Id = 3,
                    Name = "User Three",
                    CountryId = 1,
                    Gender = GenderType.Female
                    },
                    new User
                    {
                    Id = 4,
                    Name = "User Four",
                    CountryId = 2,
                    Gender = GenderType.Female
                    },
                    new User
                    {
                    Id = 5,
                    Name = "User Five",
                    CountryId = 2,
                    Gender = GenderType.Female
                    }
                };

            return users;
        }
    }
}