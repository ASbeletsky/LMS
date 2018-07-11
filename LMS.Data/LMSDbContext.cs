
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LMS.Data.Models;
using LMS.Interfaces;
namespace LMS.Data
{
    public class LMSDbContext : IdentityUserContext<User>
    {
        private readonly string connectionString;

        public LMSDbContext(IConfigReader reader)
        {
            connectionString = reader.GetConnectionString("DefaultConnection");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
