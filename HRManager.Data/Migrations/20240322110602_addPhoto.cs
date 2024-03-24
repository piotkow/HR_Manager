using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class addPhoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    PhotoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Filename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Uri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.PhotoID);
                    table.ForeignKey(
                        name: "FK_Photos_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Absences",
                keyColumn: "AbsenceID",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 3, 19, 12, 6, 0, 920, DateTimeKind.Local).AddTicks(184), new DateTime(2024, 3, 17, 12, 6, 0, 920, DateTimeKind.Local).AddTicks(179) });

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentID",
                keyValue: 1,
                column: "IssueDate",
                value: new DateTime(2022, 3, 22, 12, 6, 0, 920, DateTimeKind.Local).AddTicks(233));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 1,
                column: "DateOfEmployment",
                value: new DateTime(2022, 3, 22, 12, 6, 0, 919, DateTimeKind.Local).AddTicks(9904));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 2,
                column: "DateOfEmployment",
                value: new DateTime(2023, 3, 22, 12, 6, 0, 919, DateTimeKind.Local).AddTicks(9957));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 3,
                column: "DateOfEmployment",
                value: new DateTime(2024, 3, 22, 12, 6, 0, 919, DateTimeKind.Local).AddTicks(9962));

            migrationBuilder.InsertData(
                table: "Photos",
                columns: new[] { "PhotoID", "EmployeeID", "Filename", "Uri" },
                values: new object[,]
                {
                    { 1, 1, "JohnDoe.jpg", "https://hrmanagerblob.blob.core.windows.net/avatars/JohnDoe.jpg" },
                    { 2, 2, "AliceSmith.jpg", "https://hrmanagerblob.blob.core.windows.net/avatars/AliceSmith.jpg" },
                    { 3, 3, "BobJohnson.jpg", "https://hrmanagerblob.blob.core.windows.net/avatars/BobJohnson.jpg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Photos_EmployeeID",
                table: "Photos",
                column: "EmployeeID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Photos");

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
        }
    }
}
