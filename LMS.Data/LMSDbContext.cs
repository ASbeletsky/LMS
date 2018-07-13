using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LMS.Entries;

namespace LMS.Data
{
    public class LMSDbContext : IdentityDbContext<User>
    {
        private readonly string connectionString;

        public LMSDbContext(string connection)
        {
            connectionString = connection;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(connectionString); 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
