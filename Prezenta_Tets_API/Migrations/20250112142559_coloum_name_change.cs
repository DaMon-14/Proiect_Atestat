using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prezenta_API.Migrations
{
    /// <inheritdoc />
    public partial class coloum_name_change : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Entries",
                newName: "UserCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserCode",
                table: "Entries",
                newName: "UserId");
        }
    }
}
