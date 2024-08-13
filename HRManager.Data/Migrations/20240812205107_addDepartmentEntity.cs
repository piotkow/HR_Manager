using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class addDepartmentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Department",
                table: "Teams");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentID",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DerpartmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DerpartmentID);
                });

            migrationBuilder.UpdateData(
                table: "Absences",
                keyColumn: "AbsenceID",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 8, 9, 22, 51, 6, 744, DateTimeKind.Local).AddTicks(4624), new DateTime(2024, 8, 7, 22, 51, 6, 744, DateTimeKind.Local).AddTicks(4621) });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DerpartmentID", "Name" },
                values: new object[,]
                {
                    { 1, "Development" },
                    { 2, "Human Resources" },
                    { 3, "IT Support" }
                });

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentID",
                keyValue: 1,
                column: "IssueDate",
                value: new DateTime(2022, 8, 12, 22, 51, 6, 744, DateTimeKind.Local).AddTicks(4657));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 1,
                column: "DateOfEmployment",
                value: new DateTime(2022, 8, 12, 22, 51, 6, 744, DateTimeKind.Local).AddTicks(4537));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 2,
                column: "DateOfEmployment",
                value: new DateTime(2023, 8, 12, 22, 51, 6, 744, DateTimeKind.Local).AddTicks(4583));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 3,
                column: "DateOfEmployment",
                value: new DateTime(2024, 8, 12, 22, 51, 6, 744, DateTimeKind.Local).AddTicks(4586));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "TeamID",
                keyValue: 1,
                column: "DepartmentID",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "TeamID",
                keyValue: 2,
                column: "DepartmentID",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "TeamID",
                keyValue: 3,
                column: "DepartmentID",
                value: 3);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_DepartmentID",
                table: "Teams",
                column: "DepartmentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Departments_DepartmentID",
                table: "Teams",
                column: "DepartmentID",
                principalTable: "Departments",
                principalColumn: "DerpartmentID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Departments_DepartmentID",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Teams_DepartmentID",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "DepartmentID",
                table: "Teams");

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Absences",
                keyColumn: "AbsenceID",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 7, 20, 22, 14, 46, 942, DateTimeKind.Local).AddTicks(4397), new DateTime(2024, 7, 18, 22, 14, 46, 942, DateTimeKind.Local).AddTicks(4394) });

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentID",
                keyValue: 1,
                column: "IssueDate",
                value: new DateTime(2022, 7, 23, 22, 14, 46, 942, DateTimeKind.Local).AddTicks(4430));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 1,
                column: "DateOfEmployment",
                value: new DateTime(2022, 7, 23, 22, 14, 46, 942, DateTimeKind.Local).AddTicks(4309));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 2,
                column: "DateOfEmployment",
                value: new DateTime(2023, 7, 23, 22, 14, 46, 942, DateTimeKind.Local).AddTicks(4357));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 3,
                column: "DateOfEmployment",
                value: new DateTime(2024, 7, 23, 22, 14, 46, 942, DateTimeKind.Local).AddTicks(4359));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "TeamID",
                keyValue: 1,
                column: "Department",
                value: "Development");

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "TeamID",
                keyValue: 2,
                column: "Department",
                value: "Human Resources");

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "TeamID",
                keyValue: 3,
                column: "Department",
                value: "IT");
        }
    }
}
