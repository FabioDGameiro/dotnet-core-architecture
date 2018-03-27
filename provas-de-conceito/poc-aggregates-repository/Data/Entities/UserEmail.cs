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

        // CONSTRUCTORS

        public UserEmail(Guid userId, Email email)
        {
            UserId = userId;
            Email = email;
        }

        // PUBLIC METHODS

        public void Update(Email email)
        {
            Email = email;
        }
    }
}