using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceAPI.Migrations
{
    /// <inheritdoc />
    public partial class Database_Cleanup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "CardId",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "Client_Courses",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "Entries",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "Scanner_Courses",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "Scanners",
                keyColumn: "ScannerId",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "ClientId",
                keyValue: -1);

            migrationBuilder.DropColumn(
                name: "Salt",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Salt",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "CardId", "ClientId", "isActive" },
                values: new object[] { -1, -1, false });

            migrationBuilder.InsertData(
                table: "Client_Courses",
                columns: new[] { "Id", "ClientId", "CourseId" },
                values: new object[] { -1, -1, -1 });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "CourseDescription", "CourseName", "isActive" },
                values: new object[] { -1, "CourseDescription", "CourseName", false });

            migrationBuilder.InsertData(
                table: "Entries",
                columns: new[] { "Id", "ClientId", "CourseId", "ScanTime" },
                values: new object[] { -1, -1, -1, new DateTime(1, 1, 1, 1, 1, 1, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "Scanner_Courses",
                columns: new[] { "Id", "CourseId", "ScannerId", "isActive" },
                values: new object[] { -1, -1, -1, false });

            migrationBuilder.InsertData(
                table: "Scanners",
                columns: new[] { "ScannerId", "ScannerName", "isActive" },
                values: new object[] { -1, "ScannerName", false });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ClientId", "Email", "FirstName", "Institution", "IsAdmin", "LastName", "Password", "PhoneNumber", "Salt", "UserName" },
                values: new object[] { -1, "Email@email.com", "FirstName", "Instituion", false, "LastName", "Password", "0", "Salt", "UserName" });
        }
    }
}
