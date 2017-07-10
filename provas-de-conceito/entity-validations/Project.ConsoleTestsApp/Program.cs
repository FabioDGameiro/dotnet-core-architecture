using Project.Domain.Models;
using System;

namespace Project.ConsoleTestsApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var user = new User
            {
                Name = string.Empty,
                Email = "test@test.com"
            };

            Console.WriteLine($"User IsValid: {user.IsValid}");
            Console.WriteLine("Validation Results:");

            foreach (var error in user.ValidationResult.Errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }

            Console.ReadKey();
        }
    }
}