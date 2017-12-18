using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace poc_aggregates_repository.Data
{
    public class User : Entity
    {
        // FIELDS

        private List<UserEmail> _emails;

        // CONSTRUCTORS

        private User()
        {
            _emails = new List<UserEmail>();
        }

        // PROPERTIES

        public Guid Id { get; private set; }
        public string Name { get; private set; }

        // NAVIGATION PROPERTIES

        public IReadOnlyCollection<UserEmail> Emails => _emails.AsReadOnly();

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
            var usuarioEmail = UserEmail.Create(Id, new Email(email));

            // TODO: checar se o e-mail é válido e só adicionar se for realmente valido
            // if(!usuarioEmail.IsValid()) return;

            _emails.Add(usuarioEmail);

            // TODO: checar se o usuário continua válido
        }

        public void UpdateEmail(Guid emailId, string email)
        {
            var usuarioEmail = _emails.FirstOrDefault(x => x.Id == emailId);
            if (usuarioEmail == null) return;

            usuarioEmail.Update(new Email(email));
        }

        public void RemoveEmail(Guid emailId)
        {
            var email = _emails.FirstOrDefault(x => x.Id == emailId);
            if (email == null) return;

            email.Remove();
            //_emails.Remove(email);
        }
    }
}