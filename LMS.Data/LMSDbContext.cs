using Microsoft.EntityFrameworkCore;
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

        public DbSet<Category> Categories { get; }
        public DbSet<Question> Questions { get; }
        public DbSet<QuestionType> QuestionTypes { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Category>()
                .Property(c => c.Title)
                .IsRequired();

            modelBuilder.Entity<Question>()
                .HasKey(q => q.Id);
            modelBuilder.Entity<Question>()
                .HasOne(q => q.Category)
                .WithMany()
                .HasForeignKey(q => q.CategoryId)
                .IsRequired();
            modelBuilder.Entity<Question>()
                .HasOne(q => q.Type)
                .WithMany()
                .HasForeignKey(q => q.TypeId)
                .IsRequired();

            modelBuilder.Entity<QuestionType>()
                .HasKey(q => q.Id);
            modelBuilder.Entity<QuestionType>()
                .Property(q => q.Title).IsRequired();
        }
    }
}
