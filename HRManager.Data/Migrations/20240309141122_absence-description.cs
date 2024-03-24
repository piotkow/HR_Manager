using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class absencedescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Absences",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Absences",
                keyColumn: "AbsenceID",
                keyValue: 1,
                columns: new[] { "Description", "EndDate", "StartDate" },
                values: new object[] { "I need to take a 8 days off to complete Lego Star Wars", new DateTime(2024, 3, 6, 15, 11, 20, 850, DateTimeKind.Local).AddTicks(344), new DateTime(2024, 3, 4, 15, 11, 20, 850, DateTimeKind.Local).AddTicks(338) });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Absences");

            migrationBuilder.UpdateData(
                table: "Absences",
                keyColumn: "AbsenceID",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 2, 5, 18, 13, 44, 2, DateTimeKind.Local).AddTicks(1489), new DateTime(2024, 2, 3, 18, 13, 44, 2, DateTimeKind.Local).AddTicks(1483) });

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentID",
                keyValue: 1,
                column: "IssueDate",
                value: new DateTime(2022, 2, 8, 18, 13, 44, 2, DateTimeKind.Local).AddTicks(1606));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 1,
                column: "DateOfEmployment",
                value: new DateTime(2022, 2, 8, 18, 13, 44, 2, DateTimeKind.Local).AddTicks(1219));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 2,
                column: "DateOfEmployment",
                value: new DateTime(2023, 2, 8, 18, 13, 44, 2, DateTimeKind.Local).AddTicks(1274));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 3,
                column: "DateOfEmployment",
                value: new DateTime(2024, 2, 8, 18, 13, 44, 2, DateTimeKind.Local).AddTicks(1278));
        }
    }
}
