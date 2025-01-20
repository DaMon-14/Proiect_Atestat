using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceAPI.Migrations
{
    /// <inheritdoc />
    public partial class AdminModifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecurityAnswer",
                table: "Admins");

            migrationBuilder.RenameColumn(
                name: "SecurityQuestion",
                table: "Admins",
                newName: "Username");

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: -1,
                column: "Username",
                value: "Username");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Admins",
                newName: "SecurityQuestion");

            migrationBuilder.AddColumn<string>(
                name: "SecurityAnswer",
                table: "Admins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "SecurityAnswer", "SecurityQuestion" },
                values: new object[] { "SecurityAnswer", "SecurityQuestion" });
        }
    }
}
