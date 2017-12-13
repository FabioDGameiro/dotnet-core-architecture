using System;
using System.Collections.Generic;
using System.Linq;

namespace poc_aggregates_repository.Data
{
    public class User
    {
        private User()
        {
            Emails = new List<UserEmail>();
        }

        public User(string nome)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Emails = new List<UserEmail>();
        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public virtual ICollection<UserEmail> Emails { get; private set; }

        public void Update(string nome)
        {
            Nome = nome;
        }

        public void AddEmail(string email)
        {
            var usuarioEmail = new UserEmail(usuarioId: Id, email: email);
            Emails.Add(usuarioEmail);
        }

        public void UpdateEmail(Guid emailId, string email)
        {
            var usuarioEmail = Emails.FirstOrDefault(x => x.Id == emailId);
            if(usuarioEmail == null) return;

            usuarioEmail.Update(email);
        }

        public void RemoveEmail(Guid emailId)
        {
            var usuarioEmail = Emails.FirstOrDefault(x => x.Id == emailId);
            if(usuarioEmail == null) return;

            Emails.Remove(usuarioEmail);
        }
    }

    public class UserEmail
    {
        private UserEmail() { }

        public UserEmail(Guid usuarioId, string email)
        {
            this.Id = Guid.NewGuid();
            this.UserId = usuarioId;
            this.Email = email;
        }

        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Email { get; private set; }

        public virtual User User { get; set; }

        public void Update(string email)
        {
            Email = email;
        }
    }
}