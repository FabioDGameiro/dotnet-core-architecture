using Project.Domain.Core.Models;
using Project.Domain.Validations;

namespace Project.Domain.Models
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public User() :
            base(new UserValidator())
        {
        }
    }
}
