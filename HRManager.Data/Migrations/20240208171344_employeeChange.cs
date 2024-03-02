using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class employeeChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    PositionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PositionName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PositionDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.PositionID);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TeamDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamID);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    City = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DateOfEmployment = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PositionID = table.Column<int>(type: "int", nullable: false),
                    TeamID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeID);
                    table.ForeignKey(
                        name: "FK_Employees_Positions_PositionID",
                        column: x => x.PositionID,
                        principalTable: "Positions",
                        principalColumn: "PositionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Teams_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Teams",
                        principalColumn: "TeamID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Absences",
                columns: table => new
                {
                    AbsenceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    RejectionReason = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Absences", x => x.AbsenceID);
                    table.ForeignKey(
                        name: "FK_Absences_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountID = table.Column<int>(type: "int", nullable: false),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AccountType = table.Column<int>(type: "int", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => new { x.AccountID, x.EmployeeID });
                    table.ForeignKey(
                        name: "FK_Accounts_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    DocumentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.DocumentID);
                    table.ForeignKey(
                        name: "FK_Documents_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    ReportID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Severity = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    Result = table.Column<bool>(type: "bit", maxLength: 200, nullable: false),
                    AuthorID = table.Column<int>(type: "int", nullable: false),
                    EmployeeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.ReportID);
                    table.ForeignKey(
                        name: "FK_Reports_Employees_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reports_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "PositionID", "PositionDescription", "PositionName" },
                values: new object[,]
                {
                    { 1, "Develop software applications", "Software Engineer" },
                    { 2, "Manage HR activities", "HR Manager" },
                    { 3, "Manage and maintain system infrastructure", "System Administrator" }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "TeamID", "TeamDescription", "TeamName" },
                values: new object[,]
                {
                    { 1, "Responsible for software development", "Development" },
                    { 2, "Manages HR activities and policies", "Human Resources" },
                    { 3, "Provides IT support and infrastructure management", "IT Support" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeID", "City", "Country", "DateOfEmployment", "Email", "FirstName", "LastName", "Phone", "PositionID", "PostalCode", "Street", "TeamID" },
                values: new object[,]
                {
                    { 1, "New York", "USA", new DateTime(2022, 2, 8, 18, 13, 44, 2, DateTimeKind.Local).AddTicks(1219), "john.doe@example.com", "John", "Doe", "123-456-7890", 1, "10001", "123 Main St", 1 },
                    { 2, "Los Angeles", "USA", new DateTime(2023, 2, 8, 18, 13, 44, 2, DateTimeKind.Local).AddTicks(1274), "alice.smith@example.com", "Alice", "Smith", "987-654-3210", 2, "90001", "456 Side St", 2 },
                    { 3, "Chicago", "USA", new DateTime(2024, 2, 8, 18, 13, 44, 2, DateTimeKind.Local).AddTicks(1278), "bob.johnson@example.com", "Bob", "Johnson", "555-666-7777", 3, "60601", "789 Circle Ave", 3 }
                });

            migrationBuilder.InsertData(
                table: "Absences",
                columns: new[] { "AbsenceID", "EmployeeID", "EndDate", "RejectionReason", "StartDate", "Status" },
                values: new object[] { 1, 1, new DateTime(2024, 2, 5, 18, 13, 44, 2, DateTimeKind.Local).AddTicks(1489), null, new DateTime(2024, 2, 3, 18, 13, 44, 2, DateTimeKind.Local).AddTicks(1483), 0 });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "AccountID", "EmployeeID", "AccountType", "Password", "Username" },
                values: new object[,]
                {
                    { 1, 1, 2, "password123", "john_doe" },
                    { 2, 2, 1, "hrpassword123", "hr_user" },
                    { 3, 3, 0, "adminpassword123", "admin_user" }
                });

            migrationBuilder.InsertData(
                table: "Documents",
                columns: new[] { "DocumentID", "Content", "DocumentType", "EmployeeID", "IssueDate" },
                values: new object[] { 1, new byte[] { 76, 111, 114, 101, 109, 32, 105, 112, 115, 117, 109, 32, 100, 111, 108, 111, 114, 32, 115, 105, 116, 32, 97, 109, 101, 116, 44, 32, 99, 111, 110, 115, 101, 99, 116, 101, 116, 117, 114, 32, 97, 100, 105, 112, 105, 115, 99, 105, 110, 103, 32, 101, 108, 105, 116, 46 }, "Resume", 1, new DateTime(2022, 2, 8, 18, 13, 44, 2, DateTimeKind.Local).AddTicks(1606) });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "ReportID", "AuthorID", "Content", "EmployeeID", "Result", "Severity", "Title" },
                values: new object[] { 1, 1, "Found a critical bug in the application.", 1, true, 3, "Bug Report" });

            migrationBuilder.CreateIndex(
                name: "IX_Absences_EmployeeID",
                table: "Absences",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_EmployeeID",
                table: "Accounts",
                column: "EmployeeID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Documents_EmployeeID",
                table: "Documents",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PositionID",
                table: "Employees",
                column: "PositionID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_TeamID",
                table: "Employees",
                column: "TeamID");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_AuthorID",
                table: "Reports",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_EmployeeID",
                table: "Reports",
                column: "EmployeeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Absences");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
