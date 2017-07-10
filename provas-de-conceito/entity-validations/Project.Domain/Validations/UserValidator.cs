using FluentValidation;
using Project.Domain.Models;

namespace Project.Domain.Validations
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.Name).NotEmpty();
            RuleFor(user => user.Email).NotEmpty().EmailAddress();
        }
    }
}
