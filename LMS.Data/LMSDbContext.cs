﻿using Microsoft.EntityFrameworkCore;
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
        public DbSet<Test> Tests { get; }
        public DbSet<TestSession> Sessions { get; }

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
                .HasMany(s => s.Examenees)
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

            modelBuilder.Entity<Examenee>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<Examenee>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<User>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
