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
        public DbSet<Task> Tasks { get; }
        public DbSet<TaskType> TaskTypes { get; }

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

            modelBuilder.Entity<Task>()
                .HasKey(t => t.Id);
            modelBuilder.Entity<Task>()
                .HasOne(t => t.Category)
                .WithMany()
                .HasForeignKey(t => t.CategoryId)
                .IsRequired();
            modelBuilder.Entity<Task>()
                .HasOne(t => t.PreviousVersion)
                .WithMany();
            modelBuilder.Entity<Task>()
                .HasOne(t => t.Type)
                .WithMany()
                .HasForeignKey(t => t.TypeId)
                .IsRequired();

            modelBuilder.Entity<TaskType>()
                .HasKey(t => t.Id);
            modelBuilder.Entity<TaskType>()
                .Property(t => t.Title).IsRequired();
        }
    }
}
