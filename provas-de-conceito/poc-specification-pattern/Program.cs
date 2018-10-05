using System;
using System.Collections.Generic;
using System.Linq;
using LinqSpecs;
using poc_specification_pattern.Shared.Specifications;
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
            // parameters.Gender = GenderType.Male;
            //parameters.Query = "user2@email.com";

            var filteredUsers = FindUsers(parameters.ToSpecification());

            foreach (var user in filteredUsers)
            {
                Console.WriteLine(user);
            }
        }

        static void StaticSpecifications()
        {
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
            var activeRegisters = new ActiveRegisters<User>();
            var users = GetUsers();

            var activeSpecifiedUsers = activeRegisters && specifications;

            return users
                .Where(activeSpecifiedUsers.ToExpression().Compile())
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
                    Email = "user1@email.com",
                    CountryId = 1,
                    Gender = GenderType.Male,
                    IsRemoved = false
                },
                new User
                {
                    Id = 2,
                    Name = "User Two",
                    Email = "user2@email.com",
                    CountryId = 1,
                    Gender = GenderType.Male,
                    IsRemoved = false
                },
                new User
                {
                    Id = 3,
                    Name = "User Three",
                    Email = "user3@email.com",
                    CountryId = 1,
                    Gender = GenderType.Female,
                    IsRemoved = true
                },
                new User
                {
                    Id = 4,
                    Name = "User Four",
                    Email = "user4@email.com",
                    CountryId = 2,
                    Gender = GenderType.Female,
                    IsRemoved = false
                },
                new User
                {
                    Id = 5,
                    Name = "User Five",
                    Email = "user5@email.com",
                    CountryId = 2,
                    Gender = GenderType.Female,
                    IsRemoved = false
                }
            };

            return users;
        }
    }
}