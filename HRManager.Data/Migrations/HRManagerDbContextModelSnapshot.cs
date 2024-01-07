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
                            EmployeeID = 1,
                            EndDate = new DateTime(2024, 1, 3, 17, 12, 34, 699, DateTimeKind.Local).AddTicks(7236),
                            StartDate = new DateTime(2024, 1, 1, 17, 12, 34, 699, DateTimeKind.Local).AddTicks(7231),
                            Status = 0
                        });
                });

            modelBuilder.Entity("HRManager.Models.Entities.Account", b =>
                {
                    b.Property<int>("AccountID")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeID")
                        .HasColumnType("int");

                    b.Property<int>("AccountType")
                        .HasMaxLength(20)
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("AccountID", "EmployeeID");

                    b.HasIndex("EmployeeID")
                        .IsUnique();

                    b.ToTable("Accounts");

                    b.HasData(
                        new
                        {
                            AccountID = 1,
                            EmployeeID = 1,
                            AccountType = 2,
                            Password = "password123",
                            Username = "john_doe"
                        },
                        new
                        {
                            AccountID = 2,
                            EmployeeID = 2,
                            AccountType = 1,
                            Password = "hrpassword123",
                            Username = "hr_user"
                        },
                        new
                        {
                            AccountID = 3,
                            EmployeeID = 3,
                            AccountType = 0,
                            Password = "adminpassword123",
                            Username = "admin_user"
                        });
                });

            modelBuilder.Entity("HRManager.Models.Entities.Document", b =>
                {
                    b.Property<int>("DocumentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DocumentID"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("DocumentType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("EmployeeID")
                        .HasColumnType("int");

                    b.Property<DateTime>("IssueDate")
                        .HasColumnType("datetime2");

                    b.HasKey("DocumentID");

                    b.HasIndex("EmployeeID");

                    b.ToTable("Documents");

                    b.HasData(
                        new
                        {
                            DocumentID = 1,
                            Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                            DocumentType = "Resume",
                            EmployeeID = 1,
                            IssueDate = new DateTime(2022, 1, 6, 17, 12, 34, 699, DateTimeKind.Local).AddTicks(7300)
                        });
                });

            modelBuilder.Entity("HRManager.Models.Entities.Employee", b =>
                {
                    b.Property<int>("EmployeeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeID"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

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

                    b.Property<int>("TeamID")
                        .HasColumnType("int");

                    b.HasKey("EmployeeID");

                    b.HasIndex("PositionID");

                    b.HasIndex("TeamID");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            EmployeeID = 1,
                            Address = "123 Main St",
                            DateOfEmployment = new DateTime(2022, 1, 6, 17, 12, 34, 699, DateTimeKind.Local).AddTicks(6924),
                            Email = "john.doe@example.com",
                            FirstName = "John",
                            LastName = "Doe",
                            Phone = "123-456-7890",
                            PositionID = 1,
                            TeamID = 1
                        },
                        new
                        {
                            EmployeeID = 2,
                            Address = "456 Side St",
                            DateOfEmployment = new DateTime(2023, 1, 6, 17, 12, 34, 699, DateTimeKind.Local).AddTicks(6976),
                            Email = "alice.smith@example.com",
                            FirstName = "Alice",
                            LastName = "Smith",
                            Phone = "987-654-3210",
                            PositionID = 2,
                            TeamID = 2
                        },
                        new
                        {
                            EmployeeID = 3,
                            Address = "789 Circle Ave",
                            DateOfEmployment = new DateTime(2024, 1, 6, 17, 12, 34, 699, DateTimeKind.Local).AddTicks(6981),
                            Email = "bob.johnson@example.com",
                            FirstName = "Bob",
                            LastName = "Johnson",
                            Phone = "555-666-7777",
                            PositionID = 3,
                            TeamID = 3
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

                    b.Property<string>("Result")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

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
                            Result = "Fixed in the latest release",
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
                            TeamDescription = "Responsible for software development",
                            TeamName = "Development"
                        },
                        new
                        {
                            TeamID = 2,
                            TeamDescription = "Manages HR activities and policies",
                            TeamName = "Human Resources"
                        },
                        new
                        {
                            TeamID = 3,
                            TeamDescription = "Provides IT support and infrastructure management",
                            TeamName = "IT Support"
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
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Position");

                    b.Navigation("Team");
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
