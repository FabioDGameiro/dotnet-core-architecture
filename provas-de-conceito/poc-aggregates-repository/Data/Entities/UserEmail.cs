using System;

namespace poc_aggregates_repository.Data
{
    public class UserEmail : Entity
    {
        // PROPERTIES

        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public virtual Email Email { get; private set; }

        // NAVIGATION PROPERTIES

        public virtual User User { get; private set; }

        // FACTORIES

        public static UserEmail Create(Guid userId, Email email)
        {
            var userEmail = new UserEmail
            {
                UserId = userId,
                Email = email
            };

            // apply validations

            return userEmail;
        }

        // PUBLIC METHODS

        public void Update(Email email)
        {
            Email = email;
        }
    }
}