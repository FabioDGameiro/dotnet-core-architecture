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

        // FACTORIES

        public static UserAddress Create(Guid userId, string place, string number)
        {
            return Create(
                id: Guid.NewGuid(),
                userId: userId,
                place: place,
                number: number
            );
        }

        public static UserAddress Create(Guid id, Guid userId, string place, string number)
        {
            var userAddress = new UserAddress
            {
                Id = id,
                UserId = userId,
                Place = place,
                Number = number
            };

            // apply validations

            return userAddress;
        }

        // PUBLIC METHODS

        public void Update(string place, string number)
        {
            Place = place;
            Number = number;
        }
    }
}