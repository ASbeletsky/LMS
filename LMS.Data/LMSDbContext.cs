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

        public DbSet<Category> Categories { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionType> QuestionTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasKey(x => x.Id);

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
