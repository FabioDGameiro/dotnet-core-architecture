using System;

namespace poc_aggregates_repository.Data
{
    public class UserAddress : Entity
    {
        // PROPERTIES

        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Place { get; private set; }
        public string Number { get; private set; }

        // NAVIGATION PROPERTIES

        public virtual User User { get; set; }

        // CONSTRUCTORS

        public UserAddress(Guid userId, string place, string number)
            : this(Guid.NewGuid(), userId, place, number)
        {
        }

        public UserAddress(Guid id, Guid userId, string place, string number)
        {
            Id = id;
            UserId = userId;
            Place = place;
            Number = number;
        }

        // PUBLIC METHODS

        public void Update(string place, string number)
        {
            Place = place;
            Number = number;
        }
    }
}