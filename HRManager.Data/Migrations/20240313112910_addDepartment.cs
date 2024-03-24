using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class addDepartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                values: new object[] { new DateTime(2024, 3, 10, 12, 29, 8, 975, DateTimeKind.Local).AddTicks(5125), new DateTime(2024, 3, 8, 12, 29, 8, 975, DateTimeKind.Local).AddTicks(5120) });

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentID",
                keyValue: 1,
                column: "IssueDate",
                value: new DateTime(2022, 3, 13, 12, 29, 8, 975, DateTimeKind.Local).AddTicks(5185));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 1,
                column: "DateOfEmployment",
                value: new DateTime(2022, 3, 13, 12, 29, 8, 975, DateTimeKind.Local).AddTicks(4848));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 2,
                column: "DateOfEmployment",
                value: new DateTime(2023, 3, 13, 12, 29, 8, 975, DateTimeKind.Local).AddTicks(4906));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 3,
                column: "DateOfEmployment",
                value: new DateTime(2024, 3, 13, 12, 29, 8, 975, DateTimeKind.Local).AddTicks(4911));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "TeamID",
                keyValue: 1,
                columns: new[] { "Department", "TeamName" },
                values: new object[] { "Development", "Development Team" });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "TeamID",
                keyValue: 2,
                columns: new[] { "Department", "TeamName" },
                values: new object[] { "Human Resources", "Human Resources Team" });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "TeamID",
                keyValue: 3,
                columns: new[] { "Department", "TeamName" },
                values: new object[] { "IT", "IT Support Team" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Department",
                table: "Teams");

            migrationBuilder.UpdateData(
                table: "Absences",
                keyColumn: "AbsenceID",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 3, 6, 15, 11, 20, 850, DateTimeKind.Local).AddTicks(344), new DateTime(2024, 3, 4, 15, 11, 20, 850, DateTimeKind.Local).AddTicks(338) });

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentID",
                keyValue: 1,
                column: "IssueDate",
                value: new DateTime(2022, 3, 9, 15, 11, 20, 850, DateTimeKind.Local).AddTicks(394));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 1,
                column: "DateOfEmployment",
                value: new DateTime(2022, 3, 9, 15, 11, 20, 849, DateTimeKind.Local).AddTicks(9975));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 2,
                column: "DateOfEmployment",
                value: new DateTime(2023, 3, 9, 15, 11, 20, 850, DateTimeKind.Local).AddTicks(152));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 3,
                column: "DateOfEmployment",
                value: new DateTime(2024, 3, 9, 15, 11, 20, 850, DateTimeKind.Local).AddTicks(156));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "TeamID",
                keyValue: 1,
                column: "TeamName",
                value: "Development");

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "TeamID",
                keyValue: 2,
                column: "TeamName",
                value: "Human Resources");

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "TeamID",
                keyValue: 3,
                column: "TeamName",
                value: "IT Support");
        }
    }
}
