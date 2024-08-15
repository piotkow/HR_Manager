using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class setnull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Teams_TeamID",
                table: "Employees");

            migrationBuilder.UpdateData(
                table: "Absences",
                keyColumn: "AbsenceID",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 8, 13, 0, 5, 46, 216, DateTimeKind.Local).AddTicks(6713), new DateTime(2024, 8, 11, 0, 5, 46, 216, DateTimeKind.Local).AddTicks(6708) });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 1,
                column: "DateOfEmployment",
                value: new DateTime(2022, 8, 16, 0, 5, 46, 216, DateTimeKind.Local).AddTicks(6633));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 2,
                column: "DateOfEmployment",
                value: new DateTime(2023, 8, 16, 0, 5, 46, 216, DateTimeKind.Local).AddTicks(6683));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 3,
                column: "DateOfEmployment",
                value: new DateTime(2024, 8, 16, 0, 5, 46, 216, DateTimeKind.Local).AddTicks(6686));

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Teams_TeamID",
                table: "Employees",
                column: "TeamID",
                principalTable: "Teams",
                principalColumn: "TeamID",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Teams_TeamID",
                table: "Employees");

            migrationBuilder.UpdateData(
                table: "Absences",
                keyColumn: "AbsenceID",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 8, 13, 0, 2, 20, 690, DateTimeKind.Local).AddTicks(7503), new DateTime(2024, 8, 11, 0, 2, 20, 690, DateTimeKind.Local).AddTicks(7497) });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 1,
                column: "DateOfEmployment",
                value: new DateTime(2022, 8, 16, 0, 2, 20, 690, DateTimeKind.Local).AddTicks(7426));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 2,
                column: "DateOfEmployment",
                value: new DateTime(2023, 8, 16, 0, 2, 20, 690, DateTimeKind.Local).AddTicks(7476));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 3,
                column: "DateOfEmployment",
                value: new DateTime(2024, 8, 16, 0, 2, 20, 690, DateTimeKind.Local).AddTicks(7478));

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Teams_TeamID",
                table: "Employees",
                column: "TeamID",
                principalTable: "Teams",
                principalColumn: "TeamID");
        }
    }
}
