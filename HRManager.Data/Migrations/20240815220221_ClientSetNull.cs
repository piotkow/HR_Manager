using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class ClientSetNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Teams_TeamID",
                table: "Employees");

            migrationBuilder.DeleteData(
                table: "Documents",
                keyColumn: "DocumentID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "PhotoID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "PhotoID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "PhotoID",
                keyValue: 3);

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
                values: new object[] { new DateTime(2024, 8, 9, 22, 51, 6, 744, DateTimeKind.Local).AddTicks(4624), new DateTime(2024, 8, 7, 22, 51, 6, 744, DateTimeKind.Local).AddTicks(4621) });

            migrationBuilder.InsertData(
                table: "Documents",
                columns: new[] { "DocumentID", "EmployeeID", "Filename", "IssueDate", "Uri" },
                values: new object[] { 1, 1, "ImportantDocument", new DateTime(2022, 8, 12, 22, 51, 6, 744, DateTimeKind.Local).AddTicks(4657), "https://hrmanagerblob.blob.core.windows.net/documents/ImportantDocument.txt" });

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

            migrationBuilder.InsertData(
                table: "Photos",
                columns: new[] { "PhotoID", "EmployeeID", "Filename", "Uri" },
                values: new object[,]
                {
                    { 1, 1, "JohnDoe.jpg", "https://hrmanagerblob.blob.core.windows.net/avatars/JohnDoe.jpg" },
                    { 2, 2, "AliceSmith.jpg", "https://hrmanagerblob.blob.core.windows.net/avatars/AliceSmith.jpg" },
                    { 3, 3, "BobJohnson.jpg", "https://hrmanagerblob.blob.core.windows.net/avatars/BobJohnson.jpg" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Teams_TeamID",
                table: "Employees",
                column: "TeamID",
                principalTable: "Teams",
                principalColumn: "TeamID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
