﻿// <auto-generated />
using System;
using LMS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LMS.Data.Migrations
{
    [DbContext(typeof(LMSDbContext))]
    partial class LMSDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("LMS.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("LMS.Entities.LevelCategory", b =>
                {
                    b.Property<int>("CategoryId");

                    b.Property<int>("TestTemplateLevelId");

                    b.HasKey("CategoryId", "TestTemplateLevelId");

                    b.HasIndex("TestTemplateLevelId");

                    b.ToTable("LevelCategory");
                });

            modelBuilder.Entity("LMS.Entities.LevelTaskType", b =>
                {
                    b.Property<int>("TaskTypeId");

                    b.Property<int>("TestTemplateLevelId");

                    b.HasKey("TaskTypeId", "TestTemplateLevelId");

                    b.HasIndex("TestTemplateLevelId");

                    b.ToTable("LevelTaskType");
                });

            modelBuilder.Entity("LMS.Entities.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<int>("Complexity");

                    b.Property<string>("Content");

                    b.Property<bool>("IsActive");

                    b.Property<int?>("PreviousVersionId");

                    b.Property<int>("TypeId");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("PreviousVersionId");

                    b.HasIndex("TypeId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("LMS.Entities.TaskAnswerOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<bool>("IsCorrect");

                    b.Property<int>("TaskId");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.ToTable("TaskAnswerOption");
                });

            modelBuilder.Entity("LMS.Entities.TaskType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("TaskType");

                    b.HasData(
                        new { Id = 1, Title = "open-ended question" },
                        new { Id = 2, Title = "question with options" },
                        new { Id = 3, Title = "coding task" },
                        new { Id = 4, Title = "modelling task" }
                    );
                });

            modelBuilder.Entity("LMS.Entities.Test", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("TestTemplateId");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("TestTemplateId");

                    b.ToTable("Test");
                });

            modelBuilder.Entity("LMS.Entities.TestLevel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int>("TestId");

                    b.Property<int?>("TestTemplateLevelId");

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.HasIndex("TestTemplateLevelId");

                    b.ToTable("TestLevel");
                });

            modelBuilder.Entity("LMS.Entities.TestLevelTask", b =>
                {
                    b.Property<int>("LevelId");

                    b.Property<int>("TaskId");

                    b.HasKey("LevelId", "TaskId");

                    b.HasIndex("TaskId");

                    b.ToTable("TestLevelTask");
                });

            modelBuilder.Entity("LMS.Entities.TestTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<TimeSpan>("Duration");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("TestTemplates");
                });

            modelBuilder.Entity("LMS.Entities.TestTemplateLevel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int>("MaxComplexity");

                    b.Property<double>("MaxScore");

                    b.Property<int>("MinComplexity");

                    b.Property<int>("TasksCount");

                    b.Property<int>("TestTemplateId");

                    b.HasKey("Id");

                    b.HasIndex("TestTemplateId");

                    b.ToTable("TestTemplateLevel");
                });

            modelBuilder.Entity("LMS.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("LMS.Entities.LevelCategory", b =>
                {
                    b.HasOne("LMS.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LMS.Entities.TestTemplateLevel", "TestTemplateLevel")
                        .WithMany("Categories")
                        .HasForeignKey("TestTemplateLevelId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LMS.Entities.LevelTaskType", b =>
                {
                    b.HasOne("LMS.Entities.TaskType", "TaskType")
                        .WithMany()
                        .HasForeignKey("TaskTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LMS.Entities.TestTemplateLevel", "TestTemplateLevel")
                        .WithMany("TaskTypes")
                        .HasForeignKey("TestTemplateLevelId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LMS.Entities.Task", b =>
                {
                    b.HasOne("LMS.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("LMS.Entities.Task", "PreviousVersion")
                        .WithMany()
                        .HasForeignKey("PreviousVersionId");

                    b.HasOne("LMS.Entities.TaskType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LMS.Entities.TaskAnswerOption", b =>
                {
                    b.HasOne("LMS.Entities.Task")
                        .WithMany("AnswerOptions")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LMS.Entities.Test", b =>
                {
                    b.HasOne("LMS.Entities.TestTemplate", "TestTemplate")
                        .WithMany()
                        .HasForeignKey("TestTemplateId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("LMS.Entities.TestLevel", b =>
                {
                    b.HasOne("LMS.Entities.Test")
                        .WithMany("Levels")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LMS.Entities.TestTemplateLevel")
                        .WithMany()
                        .HasForeignKey("TestTemplateLevelId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("LMS.Entities.TestLevelTask", b =>
                {
                    b.HasOne("LMS.Entities.TestLevel", "Level")
                        .WithMany("Tasks")
                        .HasForeignKey("LevelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LMS.Entities.Task", "Task")
                        .WithMany()
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LMS.Entities.TestTemplateLevel", b =>
                {
                    b.HasOne("LMS.Entities.TestTemplate")
                        .WithMany("Levels")
                        .HasForeignKey("TestTemplateId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("LMS.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("LMS.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LMS.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("LMS.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
