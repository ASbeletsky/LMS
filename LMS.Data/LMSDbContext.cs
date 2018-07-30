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
        public DbSet<TaskType> TaskType { get; }

        public DbSet<TestTemplate> TestTemplates { get; }
        public DbSet<Test> Tests { get; }
        public DbSet<TestSession> TestSessions { get; }
        public DbSet<TaskAnswer> Answers { get; }

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
                .OnDelete(DeleteBehavior.ClientSetNull)
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
            modelBuilder.Entity<TaskType>()
                .HasData(
               new TaskType() { Title = "open-ended question", Id = (int)TaskTypes.OpenQuestion },
               new TaskType() { Title = "question with options", Id = (int)TaskTypes.OptionQuestion });

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

            modelBuilder.Entity<Test>()
                .HasKey(v => v.Id);
            modelBuilder.Entity<Test>()
                .Property(v => v.Title)
                .IsRequired();
            modelBuilder.Entity<Test>()
                .HasOne(v => v.TestTemplate)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Test>()
                .HasMany(v => v.Levels)
                .WithOne()
                .HasForeignKey(l => l.TestId);

            modelBuilder.Entity<TestLevel>()
                .HasKey(l => l.Id);
            modelBuilder.Entity<TestLevel>()
                .HasMany(l => l.Tasks)
                .WithOne(t => t.Level);
            modelBuilder.Entity<TestLevel>()
                .HasOne<TestTemplateLevel>()
                .WithMany()
                .HasForeignKey(l => l.TestTemplateLevelId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<TestLevelTask>()
                .HasKey(t => new
                {
                    t.LevelId,
                    t.TaskId
                });
            modelBuilder.Entity<TestLevelTask>()
                .HasOne(t => t.Task)
                .WithMany();

            modelBuilder.Entity<TestSession>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<TestSession>()
                .HasMany(s => s.Members)
                .WithOne(e => e.Session);

            modelBuilder.Entity<TestSession>()
                .HasMany(s => s.Tests)
                .WithOne(e => e.Session);

            modelBuilder.Entity<TestSessionTest>()
                .HasKey(t => new
                {
                    t.SessionId,
                    t.TestId
                });
            modelBuilder.Entity<TestSessionTest>()
                .HasOne(t => t.Test)
                .WithMany();

            modelBuilder.Entity<TestSessionUser>()
                .HasKey(u => new
                {
                    u.SessionId,
                    u.UserId
                });
            modelBuilder.Entity<TestSessionUser>()
                .HasOne(u => u.User)
                .WithMany();
            modelBuilder.Entity<TestSessionUser>()
                .HasOne(t => t.Test)
                .WithMany()
                .HasForeignKey(t => t.TestId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<TaskAnswerOption>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<TaskAnswerOption>()
                .HasOne<Task>()
                .WithMany(t => t.AnswerOptions)
                .HasForeignKey(k => k.TaskId);

            modelBuilder.Entity<TaskAnswer>()
                .HasKey(t => t.Id);
            modelBuilder.Entity<TaskAnswer>()
                .HasOne(t => t.TestSessionUser)
                .WithMany(t => t.Answers);

            base.OnModelCreating(modelBuilder);
        }
    }
}
