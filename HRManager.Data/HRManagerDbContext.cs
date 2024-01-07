using HRManager.Models.Entities;
using HRManager.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace HRManager.Data
{
    public class HRManagerDbContext : DbContext
    {
        public HRManagerDbContext() : base() { }

        public DbSet<Absence> Absences { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Team> Teams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HRManager;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasKey(a => new { a.AccountID, a.EmployeeID });

            // Employee ONE - ONE Account
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Account)
                .WithOne(a => a.Employee)
                .HasForeignKey<Account>(a => a.EmployeeID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // Employee ONE - MANY Absence
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Absences)
                .WithOne(a => a.Employee)
                .HasForeignKey(a => a.EmployeeID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // Konfiguracja relacji dla Report
            modelBuilder.Entity<Report>()
                .HasOne(r => r.Author)
                .WithMany(e => e.AuthoredReports)
                .HasForeignKey(r => r.AuthorID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Report>()
                .HasOne(r => r.Employee)
                .WithMany(e => e.ReportsAboutEmployee)
                .HasForeignKey(r => r.EmployeeID)
                .OnDelete(DeleteBehavior.Restrict);

            // Employee ONE - Many Document
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Documents)
                .WithOne(d => d.Employee)
                .HasForeignKey(d => d.EmployeeID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // Employee MANY - ONE Position
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Position)
                .WithMany(p => p.Employees)
                .HasForeignKey(e => e.PositionID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Employee MANY - ONE Team
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Team)
                .WithMany(t => t.Employees)
                .HasForeignKey(e => e.TeamID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Seed Employees
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    EmployeeID = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    Phone = "123-456-7890",
                    Address = "123 Main St",
                    DateOfEmployment = DateTime.Now.AddYears(-2),
                    PositionID = 1,
                    TeamID = 1
                },
                new Employee
                {
                    EmployeeID = 2,
                    FirstName = "Alice",
                    LastName = "Smith",
                    Email = "alice.smith@example.com",
                    Phone = "987-654-3210",
                    Address = "456 Side St",
                    DateOfEmployment = DateTime.Now.AddYears(-1),
                    PositionID = 2,
                    TeamID = 2
                },
                new Employee
                {
                    EmployeeID = 3,
                    FirstName = "Bob",
                    LastName = "Johnson",
                    Email = "bob.johnson@example.com",
                    Phone = "555-666-7777",
                    Address = "789 Circle Ave",
                    DateOfEmployment = DateTime.Now,
                    PositionID = 3,
                    TeamID = 3
                }
            // Add more employees as needed
            );

            // Seed Positions
            modelBuilder.Entity<Position>().HasData(
                new Position
                {
                    PositionID = 1,
                    PositionName = "Software Engineer",
                    PositionDescription = "Develop software applications"
                },
                new Position
                {
                    PositionID = 2,
                    PositionName = "HR Manager",
                    PositionDescription = "Manage HR activities"
                },
                new Position
                {
                    PositionID = 3,
                    PositionName = "System Administrator",
                    PositionDescription = "Manage and maintain system infrastructure"
                }
            // Add more positions as needed
            );

            // Seed Absences
            modelBuilder.Entity<Absence>().HasData(
                new Absence
                {
                    AbsenceID = 1,
                    EmployeeID = 1,
                    StartDate = DateTime.Now.AddDays(-5),
                    EndDate = DateTime.Now.AddDays(-3),
                    Status = Status.Approved,
                    RejectionReason = null
                }
                // Add more absences as needed
            );

            // Seed Accounts
            modelBuilder.Entity<Account>().HasData(
                new Account
                {
                    AccountID = 1,
                    EmployeeID = 1,
                    Username = "john_doe",
                    Password = "password123",
                    AccountType = Role.Employee
                },
                new Account
                {
                    AccountID = 2,
                    EmployeeID = 2,
                    Username = "hr_user",
                    Password = "hrpassword123",
                    AccountType = Role.HR
                },
                new Account
                {
                    AccountID = 3,
                    EmployeeID = 3,
                    Username = "admin_user",
                    Password = "adminpassword123",
                    AccountType = Role.Admin
                }
            // Add more accounts as needed
            );

            // Seed Documents
            modelBuilder.Entity<Document>().HasData(
                new Document
                {
                    DocumentID = 1,
                    EmployeeID = 1,
                    DocumentType = "Resume",
                    IssueDate = DateTime.Now.AddYears(-2),
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit."
                }
                // Add more documents as needed
            );

            // Seed Reports
            modelBuilder.Entity<Report>().HasData(
                new Report
                {
                    ReportID = 1,
                    Title = "Bug Report",
                    Content = "Found a critical bug in the application.",
                    Severity = Severity.Major,
                    Result = "Fixed in the latest release",
                    AuthorID = 1,
                    EmployeeID = 1
                }
            );

            modelBuilder.Entity<Team>().HasData(
                new Team
                {
                    TeamID = 1,
                    TeamName = "Development",
                    TeamDescription = "Responsible for software development"
                },
                new Team
                {
                    TeamID = 2,
                    TeamName = "Human Resources",
                    TeamDescription = "Manages HR activities and policies"
                },
                new Team
                {
                    TeamID = 3,
                    TeamName = "IT Support",
                    TeamDescription = "Provides IT support and infrastructure management"
                }
            // Add more teams as needed
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
