using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prezenta_API.Migrations
{
    /// <inheritdoc />
    public partial class ScannerIdadd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ScannerId",
                table: "Entries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Entries",
                keyColumn: "Id",
                keyValue: -1,
                column: "ScannerId",
                value: -1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScannerId",
                table: "Entries");
        }
    }
}
