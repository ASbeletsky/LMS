using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
        public DbSet<Task> Tasks { get; }
        public DbSet<TaskType> TaskTypes { get; }
        
        public DbSet<TestTemplate> TestTemplates { get; }
        
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
            modelBuilder.Entity<TestTemplate>()
                .HasMany(t => t.Levels)
                .WithOne()
                .HasForeignKey(l => l.TestTemplateId);
            
            modelBuilder.Entity<TestTemplateLevel>()
                .HasKey(l => l.Id);
            modelBuilder.Entity<TestTemplateLevel>()
                .HasMany(f => f.Categories)
                .WithOne(c => c.TestTemplateLevel)
                .HasForeignKey(c => c.TestTemplateLevelId);
            modelBuilder.Entity<TestTemplateLevel>()
                .HasMany(f => f.TaskTypes)
                .WithOne(t => t.TestTemplateLevel)
                .HasForeignKey(t => t.TestTemplateLevelId);
            
            modelBuilder.Entity<LevelCategory>()
                .HasKey(c => new
                {
                    c.CategoryId,
                    c.TestTemplateLevelId
                });
            modelBuilder.Entity<LevelCategory>()
                .HasOne(c => c.Category)
                .WithMany();

            modelBuilder.Entity<LevelTaskType>()
                .HasKey(t => new
                {
                    t.TaskTypeId,
                    t.TestTemplateLevelId
                });
            modelBuilder.Entity<LevelTaskType>()
                .HasOne(c => c.TaskType)
                .WithMany();

            modelBuilder.Entity<User>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
