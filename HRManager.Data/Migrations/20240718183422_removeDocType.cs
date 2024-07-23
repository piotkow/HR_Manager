using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class removeDocType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentType",
                table: "Documents");

            migrationBuilder.UpdateData(
                table: "Absences",
                keyColumn: "AbsenceID",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 7, 15, 20, 34, 21, 658, DateTimeKind.Local).AddTicks(2583), new DateTime(2024, 7, 13, 20, 34, 21, 658, DateTimeKind.Local).AddTicks(2579) });

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentID",
                keyValue: 1,
                column: "IssueDate",
                value: new DateTime(2022, 7, 18, 20, 34, 21, 658, DateTimeKind.Local).AddTicks(2629));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 1,
                column: "DateOfEmployment",
                value: new DateTime(2022, 7, 18, 20, 34, 21, 658, DateTimeKind.Local).AddTicks(2442));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 2,
                column: "DateOfEmployment",
                value: new DateTime(2023, 7, 18, 20, 34, 21, 658, DateTimeKind.Local).AddTicks(2529));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 3,
                column: "DateOfEmployment",
                value: new DateTime(2024, 7, 18, 20, 34, 21, 658, DateTimeKind.Local).AddTicks(2532));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DocumentType",
                table: "Documents",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Absences",
                keyColumn: "AbsenceID",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 5, 23, 19, 42, 43, 456, DateTimeKind.Local).AddTicks(3454), new DateTime(2024, 5, 21, 19, 42, 43, 456, DateTimeKind.Local).AddTicks(3451) });

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentID",
                keyValue: 1,
                columns: new[] { "DocumentType", "IssueDate" },
                values: new object[] { "Resume", new DateTime(2022, 5, 26, 19, 42, 43, 456, DateTimeKind.Local).AddTicks(3490) });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 1,
                column: "DateOfEmployment",
                value: new DateTime(2022, 5, 26, 19, 42, 43, 456, DateTimeKind.Local).AddTicks(3360));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 2,
                column: "DateOfEmployment",
                value: new DateTime(2023, 5, 26, 19, 42, 43, 456, DateTimeKind.Local).AddTicks(3411));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 3,
                column: "DateOfEmployment",
                value: new DateTime(2024, 5, 26, 19, 42, 43, 456, DateTimeKind.Local).AddTicks(3414));
        }
    }
}
