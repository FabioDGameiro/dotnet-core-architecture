using FluentValidation;
using FluentValidation.Results;
using System;

namespace Project.Domain.Core.Models
{
    public abstract class Entity
    {
        // Construtor

        protected Entity()
        {
        }

        protected Entity(IValidator validator)
        {
            Validator = validator;
        }

        // Propriedades

        public Guid Id { get; set; }

        // Validações

        public IValidator Validator { get; set; }
        public ValidationResult ValidationResult { get; set; }
        public bool IsValid
        {
            get
            {
                ValidationResult = Validator.Validate(this);
                return ValidationResult.IsValid;
            }
        }
    }
}
