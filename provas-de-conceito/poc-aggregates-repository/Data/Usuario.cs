using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace poc_aggregates_repository.Data
{
    public class User
    {
        // FIELDS

        private readonly List<UserEmail> _emails;

        // CONSTRUCTORS

        private User()
        {
            _emails = new List<UserEmail>();
        }

        // PROPERTIES

        public Guid Id { get; private set; }
        public string Name { get; private set; }

        // NAVIGATION PROPERTIES

        public virtual ReadOnlyCollection<UserEmail> Emails => _emails.AsReadOnly();

        // FACTORIES

        public static User Create(string name)
        {
            return Create(
                Guid.NewGuid(),
                name
            );
        }

        public static User Create(Guid id, string name)
        {
            var user = new User
            {
                Id = id,
                Name = name
            };

            // apply validations

            return user;
        }

        // PUBLIC METHODS

        public void Update(string name)
        {
            Name = name;
        }

        public void AddEmail(string email)
        {
            var usuarioEmail = new UserEmail(usuarioId: Id, email: email);

            // TODO: checar se o e-mail é válido e só adicionar se for realmente valido
            // if(!usuarioEmail.IsValid()) return;

            _emails.Add(usuarioEmail);

            // TODO: checar se o usuário continua válido
        }

        public void UpdateEmail(Guid emailId, string email)
        {
            var usuarioEmail = _emails.FirstOrDefault(x => x.Id == emailId);
            if (usuarioEmail == null) return;

            usuarioEmail.Update(email);
        }

        public void RemoveEmail(Guid emailId)
        {
            var usuarioEmail = _emails.FirstOrDefault(x => x.Id == emailId);
            if (usuarioEmail == null) return;

            _emails.Remove(usuarioEmail);
        }
    }

    public class UserEmail
    {
        // PROPERTIES

        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Email { get; private set; }

        // NAVIGATION PROPERTIES

        public virtual User User { get; set; }

        // FACTORIES

        public static UserEmail Create(Guid userId, string email)
        {
            return Create(
                Guid.NewGuid(),
                userId,
                email
            );
        }

        public static UserEmail Create(Guid id, Guid userId, string email)
        {
            var userEmail = new UserEmail
            {
                Id = id,
                UserId = userId,
                Email = email
            };

            // apply validations

            return userEmail;
        }

        // PUBLIC METHODS

        public void Update(string email)
        {
            Email = email;
        }
    }

    public class UserAddress
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
                number:number
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