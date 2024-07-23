﻿// <auto-generated />
using System;
using HRManager.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HRManager.Data.Migrations
{
    [DbContext(typeof(HRManagerDbContext))]
    partial class HRManagerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HRManager.Models.Entities.Absence", b =>
                {
                    b.Property<int>("AbsenceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AbsenceID"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmployeeID")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("RejectionReason")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.HasKey("AbsenceID");

                    b.HasIndex("EmployeeID");

                    b.ToTable("Absences");

                    b.HasData(
                        new
                        {
                            AbsenceID = 1,
                            Description = "I need to take a 8 days off to complete Lego Star Wars",
                            EmployeeID = 1,
                            EndDate = new DateTime(2024, 7, 20, 22, 14, 46, 942, DateTimeKind.Local).AddTicks(4397),
                            StartDate = new DateTime(2024, 7, 18, 22, 14, 46, 942, DateTimeKind.Local).AddTicks(4394),
                            Status = 0
                        });
                });

            modelBuilder.Entity("HRManager.Models.Entities.Account", b =>
                {
                    b.Property<int>("AccountID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AccountID"));

                    b.Property<int>("AccountType")
                        .HasMaxLength(20)
                        .HasColumnType("int");

                    b.Property<int>("EmployeeID")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("AccountID");

                    b.HasIndex("EmployeeID")
                        .IsUnique();

                    b.ToTable("Accounts");

                    b.HasData(
                        new
                        {
                            AccountID = 1,
                            AccountType = 2,
                            EmployeeID = 1,
                            Password = "$2a$10$YxDw2YmiG1DheMCs2UlhjOMSWrr.SXMHdGu..6JNC8n/vnFAkVqDK",
                            Username = "emp"
                        },
                        new
                        {
                            AccountID = 2,
                            AccountType = 1,
                            EmployeeID = 2,
                            Password = "$2a$10$q948TuYKqihClxWMU9WwXuVRebJiPYm8PCUXby9pGAatGHYez6aAi",
                            Username = "hr"
                        },
                        new
                        {
                            AccountID = 3,
                            AccountType = 0,
                            EmployeeID = 3,
                            Password = "$2a$10$sAop1QcdbP3y4OtC60pwSuVfhe6q1MjCoJJfvLcaulNs/cZ5Ewodu",
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("HRManager.Models.Entities.Document", b =>
                {
                    b.Property<int>("DocumentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DocumentID"));

                    b.Property<int>("EmployeeID")
                        .HasColumnType("int");

                    b.Property<string>("Filename")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("IssueDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Uri")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DocumentID");

                    b.HasIndex("EmployeeID");

                    b.ToTable("Documents");

                    b.HasData(
                        new
                        {
                            DocumentID = 1,
                            EmployeeID = 1,
                            Filename = "ImportantDocument",
                            IssueDate = new DateTime(2022, 7, 23, 22, 14, 46, 942, DateTimeKind.Local).AddTicks(4430),
                            Uri = "https://hrmanagerblob.blob.core.windows.net/documents/ImportantDocument.txt"
                        });
                });

            modelBuilder.Entity("HRManager.Models.Entities.Employee", b =>
                {
                    b.Property<int>("EmployeeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeID"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("DateOfEmployment")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PositionID")
                        .HasColumnType("int");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int?>("TeamID")
                        .HasColumnType("int");

                    b.HasKey("EmployeeID");

                    b.HasIndex("PositionID");

                    b.HasIndex("TeamID");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            EmployeeID = 1,
                            City = "New York",
                            Country = "USA",
                            DateOfEmployment = new DateTime(2022, 7, 23, 22, 14, 46, 942, DateTimeKind.Local).AddTicks(4309),
                            Email = "john.doe@example.com",
                            FirstName = "John",
                            LastName = "Doe",
                            Phone = "123-456-7890",
                            PositionID = 1,
                            PostalCode = "10001",
                            Street = "123 Main St",
                            TeamID = 1
                        },
                        new
                        {
                            EmployeeID = 2,
                            City = "Los Angeles",
                            Country = "USA",
                            DateOfEmployment = new DateTime(2023, 7, 23, 22, 14, 46, 942, DateTimeKind.Local).AddTicks(4357),
                            Email = "alice.smith@example.com",
                            FirstName = "Alice",
                            LastName = "Smith",
                            Phone = "987-654-3210",
                            PositionID = 2,
                            PostalCode = "90001",
                            Street = "456 Side St",
                            TeamID = 2
                        },
                        new
                        {
                            EmployeeID = 3,
                            City = "Chicago",
                            Country = "USA",
                            DateOfEmployment = new DateTime(2024, 7, 23, 22, 14, 46, 942, DateTimeKind.Local).AddTicks(4359),
                            Email = "bob.johnson@example.com",
                            FirstName = "Bob",
                            LastName = "Johnson",
                            Phone = "555-666-7777",
                            PositionID = 3,
                            PostalCode = "60601",
                            Street = "789 Circle Ave",
                            TeamID = 3
                        });
                });

            modelBuilder.Entity("HRManager.Models.Entities.Photo", b =>
                {
                    b.Property<int>("PhotoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PhotoID"));

                    b.Property<int>("EmployeeID")
                        .HasColumnType("int");

                    b.Property<string>("Filename")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Uri")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PhotoID");

                    b.HasIndex("EmployeeID")
                        .IsUnique();

                    b.ToTable("Photos");

                    b.HasData(
                        new
                        {
                            PhotoID = 1,
                            EmployeeID = 1,
                            Filename = "JohnDoe.jpg",
                            Uri = "https://hrmanagerblob.blob.core.windows.net/avatars/JohnDoe.jpg"
                        },
                        new
                        {
                            PhotoID = 2,
                            EmployeeID = 2,
                            Filename = "AliceSmith.jpg",
                            Uri = "https://hrmanagerblob.blob.core.windows.net/avatars/AliceSmith.jpg"
                        },
                        new
                        {
                            PhotoID = 3,
                            EmployeeID = 3,
                            Filename = "BobJohnson.jpg",
                            Uri = "https://hrmanagerblob.blob.core.windows.net/avatars/BobJohnson.jpg"
                        });
                });

            modelBuilder.Entity("HRManager.Models.Entities.Position", b =>
                {
                    b.Property<int>("PositionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PositionID"));

                    b.Property<string>("PositionDescription")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("PositionName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("PositionID");

                    b.ToTable("Positions");

                    b.HasData(
                        new
                        {
                            PositionID = 1,
                            PositionDescription = "Develop software applications",
                            PositionName = "Software Engineer"
                        },
                        new
                        {
                            PositionID = 2,
                            PositionDescription = "Manage HR activities",
                            PositionName = "HR Manager"
                        },
                        new
                        {
                            PositionID = 3,
                            PositionDescription = "Manage and maintain system infrastructure",
                            PositionName = "System Administrator"
                        });
                });

            modelBuilder.Entity("HRManager.Models.Entities.Report", b =>
                {
                    b.Property<int>("ReportID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReportID"));

                    b.Property<int>("AuthorID")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("EmployeeID")
                        .HasColumnType("int");

                    b.Property<bool>("Result")
                        .HasMaxLength(200)
                        .HasColumnType("bit");

                    b.Property<int>("Severity")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ReportID");

                    b.HasIndex("AuthorID");

                    b.HasIndex("EmployeeID");

                    b.ToTable("Reports");

                    b.HasData(
                        new
                        {
                            ReportID = 1,
                            AuthorID = 1,
                            Content = "Found a critical bug in the application.",
                            EmployeeID = 1,
                            Result = true,
                            Severity = 3,
                            Title = "Bug Report"
                        });
                });

            modelBuilder.Entity("HRManager.Models.Entities.Team", b =>
                {
                    b.Property<int>("TeamID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TeamID"));

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeamDescription")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("TeamName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("TeamID");

                    b.ToTable("Teams");

                    b.HasData(
                        new
                        {
                            TeamID = 1,
                            Department = "Development",
                            TeamDescription = "Responsible for software development",
                            TeamName = "Development Team"
                        },
                        new
                        {
                            TeamID = 2,
                            Department = "Human Resources",
                            TeamDescription = "Manages HR activities and policies",
                            TeamName = "Human Resources Team"
                        },
                        new
                        {
                            TeamID = 3,
                            Department = "IT",
                            TeamDescription = "Provides IT support and infrastructure management",
                            TeamName = "IT Support Team"
                        });
                });

            modelBuilder.Entity("HRManager.Models.Entities.Absence", b =>
                {
                    b.HasOne("HRManager.Models.Entities.Employee", "Employee")
                        .WithMany("Absences")
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("HRManager.Models.Entities.Account", b =>
                {
                    b.HasOne("HRManager.Models.Entities.Employee", "Employee")
                        .WithOne("Account")
                        .HasForeignKey("HRManager.Models.Entities.Account", "EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("HRManager.Models.Entities.Document", b =>
                {
                    b.HasOne("HRManager.Models.Entities.Employee", "Employee")
                        .WithMany("Documents")
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("HRManager.Models.Entities.Employee", b =>
                {
                    b.HasOne("HRManager.Models.Entities.Position", "Position")
                        .WithMany("Employees")
                        .HasForeignKey("PositionID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("HRManager.Models.Entities.Team", "Team")
                        .WithMany("Employees")
                        .HasForeignKey("TeamID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Position");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("HRManager.Models.Entities.Photo", b =>
                {
                    b.HasOne("HRManager.Models.Entities.Employee", "Employee")
                        .WithOne("Photo")
                        .HasForeignKey("HRManager.Models.Entities.Photo", "EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("HRManager.Models.Entities.Report", b =>
                {
                    b.HasOne("HRManager.Models.Entities.Employee", "Author")
                        .WithMany("AuthoredReports")
                        .HasForeignKey("AuthorID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("HRManager.Models.Entities.Employee", "Employee")
                        .WithMany("ReportsAboutEmployee")
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("HRManager.Models.Entities.Employee", b =>
                {
                    b.Navigation("Absences");

                    b.Navigation("Account")
                        .IsRequired();

                    b.Navigation("AuthoredReports");

                    b.Navigation("Documents");

                    b.Navigation("Photo")
                        .IsRequired();

                    b.Navigation("ReportsAboutEmployee");
                });

            modelBuilder.Entity("HRManager.Models.Entities.Position", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("HRManager.Models.Entities.Team", b =>
                {
                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
