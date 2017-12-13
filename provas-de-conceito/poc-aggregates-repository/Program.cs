using System;
using Microsoft.EntityFrameworkCore;
using poc_aggregates_repository.Data;

namespace poc_aggregates_repository
{
    class Program
    {
        static void Main(string[] args)
        {
            //CreateUser();
            //UpdateUser();
            //AddEmailForExistingUser();
            //UpdateEmailForExistingUser();
            //RemoveEmailForExistingUser();
        }

        static void CreateUser()
        {
            var user = new User("Tiago Santos");
            user.AddEmail("tiagosantos@outlook.com");
            user.AddEmail("taigobrasil@gmail.com");

            using(var repository = new UserRepository())
            {
                repository.AddUser(user);

                if (repository.Save() > 0)
                {
                    Console.WriteLine("User created!");
                };
            }
        }

        static void UpdateUser()
        {
            using(var repository = new UserRepository())
            {
                var user = repository.GetUserById(new Guid("4d862c56-bd77-4093-a818-4546390fba83"));

                if (user == null)
                {
                    Console.WriteLine("User not found");
                    return;
                }

                user.Update(nome: "Tiago Santos (upd3)");

                if (repository.Save() > 0)
                {
                    Console.WriteLine("User updated!");
                };
            }
        }

        static void AddEmailForExistingUser()
        {
            var userId = new Guid("f0cd6e3e-b95b-4dab-bb0b-7e6c6e1b0855");

            using(var repository = new UserRepository())
            {
                var user = repository.GetUserById(userId);

                if (user == null)
                {
                    Console.WriteLine("User not found");
                    return;
                }

                user.AddEmail("tiago@email.com");

                repository.UpdateUser(user);

                if (repository.Save() > 0)
                {
                    Console.WriteLine("E-mail added!");
                };
            }
        }

        static void UpdateEmailForExistingUser()
        {
            var userId = new Guid("f0cd6e3e-b95b-4dab-bb0b-7e6c6e1b0855");
            var emailId = new Guid("804aff75-8e48-4f53-b55d-8d3ca76a2df9");

            using(var repository = new UserRepository())
            {
                var user = repository.GetUserById(userId);

                if (user == null)
                {
                    Console.WriteLine("User not found");
                    return;
                }

                user.UpdateEmail(emailId, "updated1@email.com");

                repository.UpdateUser(user);

                if (repository.Save() > 0)
                {
                    Console.WriteLine("E-mail updated!");
                };
            }
        }

        static void RemoveEmailForExistingUser()
        {
            var emailId = new Guid("b520c665-02d7-45a0-b18d-1ec1ca59438d");
            using(var repository = new UserRepository())
            {
                var user = repository.GetUserById(new Guid("4d862c56-bd77-4093-a818-4546390fba83"));

                if (user == null)
                {
                    Console.WriteLine("User not found");
                    return;
                }

                user.RemoveEmail(emailId);

                if (repository.Save() > 0)
                {
                    Console.WriteLine("E-mail removed!");
                };
            }
        }
    }
}