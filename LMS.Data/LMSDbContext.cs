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

            modelBuilder.Entity<TestTemplate>()
                .HasKey(t => t.Id);
            modelBuilder.Entity<TestTemplate>()
                .Property(t => t.Title)
                .IsRequired();

            modelBuilder.Entity<Level>()
                .HasKey(l => l.Id);
            modelBuilder.Entity<Level>()
                .HasOne(l => l.TestTemplate);
            modelBuilder.Entity<Level>()
                .HasMany(l => l.Categories)
                .WithOne(c => c.Level);
            modelBuilder.Entity<Level>()
                .HasMany(l => l.TaskTypes)
                .WithOne(t => t.Level);

            modelBuilder.Entity<LevelCategory>()
                .HasKey(c => new
                {
                    c.CategoryId,
                    c.LevelId
                });
            modelBuilder.Entity<LevelCategory>()
                .HasOne(c => c.Category);

            modelBuilder.Entity<LevelTaskType>()
                .HasKey(t => new
                {
                    t.TaskTypeId,
                    t.LevelId
                });
            modelBuilder.Entity<LevelTaskType>()
                .HasOne(c => c.TaskType);
        }
    }
}
