using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace poc_aggregates_repository.Data
{
    public class UsersContext : DbContext
    {
        public UsersContext()
        {
        }

        public UsersContext(DbContextOptions<UsersContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserEmail> UsersEmails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=UsersDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().Property(x => x.Nome).IsRequired().HasColumnType("varchar(20)");

            modelBuilder.Entity<UserEmail>().HasKey(x => x.Id);
            modelBuilder.Entity<UserEmail>().Property(x => x.Email).IsRequired().HasColumnType("varchar(100)");
            modelBuilder.Entity<UserEmail>().HasOne(x => x.User).WithMany(x => x.Emails).HasForeignKey(x => x.UserId);
        }
    }
}