using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace poc_aggregates_repository.Data
{
    public class UserRepository : IDisposable
    {
        private readonly UsersContext _context;

        public UserRepository()
        {
            _context = new UsersContext();
        }

        public void AddUser(User usuario)
        {
            _context.Users.Add(usuario);
        }

        public void UpdateUser(User usuario)
        {
            foreach (var userEmail in usuario.Emails.Where(x => x.IsRemoved()))
                _context.UsersEmails.Remove(userEmail);

            _context.Users.Update(usuario);
        }

        public User GetUserById(Guid usuarioId)
        {
            return _context.Users.AsNoTracking()
                .Include(x => x.Emails).AsNoTracking()
                .FirstOrDefault(x => x.Id == usuarioId);
        }

        public bool UserExists(Guid usuarioId)
        {
            return _context.Users.Any(x => x.Id == usuarioId);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}