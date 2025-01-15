using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prezenta_API.Migrations
{
    /// <inheritdoc />
    public partial class UserId_add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Entries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Entries",
                keyColumn: "Id",
                keyValue: -1,
                column: "UserId",
                value: -1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Entries");
        }
    }
}
