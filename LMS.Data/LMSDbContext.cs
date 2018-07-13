using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LMS.Entities;

namespace LMS.Data
{
    public class LMSDbContext : IdentityDbContext<User>
    {
        private readonly string connectionString;

        public LMSDbContext(string connection)
        {
            connectionString = connection;
        }

        public DbSet<Category> Categories { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(connectionString); 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasKey(c => c.Id);
            modelBuilder.Entity<Category>().Property(c => c.Title).IsRequired();

            modelBuilder.Entity<User>();
        }
    }
}
