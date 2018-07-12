using Microsoft.EntityFrameworkCore;
using LMS.Interfaces;
using LMS.Data.Models;

namespace LMS.Data
{
    public class LMSDbContext : DbContext
    {
        private readonly string connectionString;

        public LMSDbContext(IConfigReader reader)
        {
            connectionString = reader.GetConnectionString("DefaultConnection");
        }

        public DbSet<Test> Test { get; set; }
        public DbSet<TestProblem> Problem { get; set; }
        public DbSet<Choice> Choice { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseMySQL(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Test>().HasKey(x=>x.Id);
            modelBuilder.Entity<Test>().HasOne(x=>x.Category);
            modelBuilder.Entity<Test>().HasMany(x => x.Problems);
            modelBuilder.Entity<TestCategory>().HasKey(x => x.Id);
            modelBuilder.Entity<TestProblem>().HasKey(x => x.Id);
            modelBuilder.Entity<TestProblem>().HasOne(x => x.Type);
            modelBuilder.Entity<TestProblem>().HasMany(x => x.Choices);
            modelBuilder.Entity<ProblemType>().HasKey(x=>x.Id);
        }
    }
}
