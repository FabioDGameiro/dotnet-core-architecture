using System;
using Microsoft.EntityFrameworkCore;
using poc_aggregates_repository.Data;

namespace poc_aggregates_repository
{
    class Program
    {
        // REGRA DE NEGÓCIO =)

        // Criar uma representação de um usuário (Usando boas práticas do DDD) onde:

        // 1. Este usuário deverá possuir as propriedades (Id, Name).
        // 2. Este usuário deverá possuir uma lista de e-mails com as propriedades (Id, UserId, Email).
        // 3. Este usuário deverá possuir de endereços com as propriedades (Id, UserId, Address, Number).

        // 4. O usuário deverá ter no mínimo 1 ou no máximo 3 e-mails cadastrados.
        // 5. O usuário deverá ter 1 e somente 1 e-mail definido como principal.
        
        // 6. O usuário deverá ter no mínimo 1 ou no máximo 2 endereços cadastrados.
        // 7. O usuário deverá ter 1 e somente 1 endereço definido como principal.

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