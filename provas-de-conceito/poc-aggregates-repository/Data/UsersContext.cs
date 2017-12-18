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

            modelBuilder.Entity<User>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<User>()
                .Metadata.FindNavigation(nameof(User.Emails)).SetPropertyAccessMode(PropertyAccessMode.Field);

            modelBuilder.Entity<User>()
                .Property(x => x.Name)
                .HasColumnType("varchar(20)")
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasMany(x => x.Emails)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .IsRequired();

            modelBuilder.Entity<UserEmail>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<UserEmail>()
                .Property(x=>x.Email)
                .HasColumnType("varchar(100)")
                .IsRequired();

            //modelBuilder.Entity<UserEmail>().OwnsOne(x => x.Email, cb =>
            //{
            //    cb.Property(x => x.Address)
            //        .HasColumnType("varchar(100)")
            //        .IsRequired();
            //});
        }
    }
}