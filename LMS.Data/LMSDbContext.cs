using Microsoft.EntityFrameworkCore;
using LMS.Interfaces;
using LMS.Entities;

namespace LMS.Data
{
    public class LMSDbContext : DbContext
    {
        private readonly string connectionString;

        public LMSDbContext(string connection)
        {
            connectionString = connection;
        }

        public DbSet<Test> Test { get; set; }
        public DbSet<Question> Problem { get; set; }
        public DbSet<Choice> Choice { get; set; }
        public DbSet<TestCategory> TestCategory { get; set; }
        public DbSet<QuestionType> QuestionType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Test>().HasKey(x=>x.Id);
            modelBuilder.Entity<Test>().HasOne(x=>x.Category);
            modelBuilder.Entity<Test>().HasMany(x => x.Questions);
            modelBuilder.Entity<TestCategory>().HasKey(x => x.Id);
            modelBuilder.Entity<Question>().HasKey(x => x.Id);
            modelBuilder.Entity<Question>().HasOne(x => x.Type);
            modelBuilder.Entity<Question>().HasMany(x => x.Choices);
            modelBuilder.Entity<QuestionType>().HasKey(x=>x.Id);
        }
    }
}
