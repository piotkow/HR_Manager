using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class nullableTeam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TeamID",
                table: "Employees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TeamID",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
    }
}
