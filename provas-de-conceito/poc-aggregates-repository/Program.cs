using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using poc_aggregates_repository.Data;

namespace poc_aggregates_repository
{
    internal class Program
    {
        // REGRA DE NEGÓCIO =)

        // Criar uma representação de um usuário (Usando boas práticas do DDD) onde:

        // 1. Este usuário deverá possuir as propriedades (Id, Name).
        // 2. Este usuário deverá possuir uma lista de e-mails com as propriedades (Id, UserId, Email).
        // 3. Este usuário deverá possuir de endereços com as propriedades (Id, UserId, Place, Number).

        // 4. O usuário deverá ter no mínimo 1 ou no máximo 3 e-mails cadastrados.
        // 5. O usuário deverá ter 1 e somente 1 e-mail definido como principal.

        // 6. O usuário deverá ter no mínimo 1 ou no máximo 2 endereços cadastrados.
        // 7. O usuário deverá ter 1 e somente 1 endereço definido como principal.

        private static readonly Guid UserId = new Guid("e6e84909-c57a-4d68-8edf-b858cdd0634d");

        private static void Main(string[] args)
        {
             //CreateUser();
            // UpdateUser();
            //AddEmailForExistingUser();
            // UpdateEmailForExistingUser();
            RemoveEmailForExistingUser(new Guid("64D6F516-FB95-4A62-6B46-08D545B359D7"));
        }

        private static void CreateUser()
        {
            var user = User.Create(UserId, "Tiago");

            user.AddEmail("tiagosantos@outlook.com");
            user.AddEmail("taigobrasil@gmail.com");

            using (var repository = new UserRepository())
            {
                repository.AddUser(user);

                if (repository.Save() > 0)
                {
                    Console.WriteLine("User created!");
                }
            }
        }

        private static void UpdateUser()
        {
            using (var repository = new UserRepository())
            {
                var user = repository.GetUserById(UserId);

                if (user == null)
                {
                    Console.WriteLine("User not found");
                    return;
                }

                user.Update("Tiago Santos (upd3)");

                repository.UpdateUser(user);

                if (repository.Save() > 0)
                {
                    Console.WriteLine("User updated!");
                };
            }
        }

        private static void AddEmailForExistingUser()
        {
            using (var repository = new UserRepository())
            {
                var user = repository.GetUserById(UserId);

                if (user == null)
                {
                    Console.WriteLine("User not found");
                    return;
                }

                user.AddEmail("tiagosantos@email.com");

                repository.UpdateUser(user);

                if (repository.Save() > 0)
                {
                    Console.WriteLine("E-mail added!");
                };
            }
        }

        private static void UpdateEmailForExistingUser()
        {
            var emailId = new Guid("3d23b922-ee11-46ca-791f-08d54592f5dd");

            using (var repository = new UserRepository())
            {
                var user = repository.GetUserById(UserId);

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

        private static void RemoveEmailForExistingUser(Guid emailId)
        {
            using (var repository = new UserRepository())
            {
                var user = repository.GetUserById(UserId);

                if (user == null)
                {
                    Console.WriteLine("User not found");
                    return;
                }

                user.RemoveEmail(emailId);

                repository.UpdateUser(user);

                if (repository.Save() > 0)
                {
                    Console.WriteLine("E-mail removed!");
                };
            }
        }
    }
}