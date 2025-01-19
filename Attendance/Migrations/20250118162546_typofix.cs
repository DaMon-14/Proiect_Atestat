using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceAPI.Migrations
{
    /// <inheritdoc />
    public partial class typofix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Institutuion",
                table: "Clients",
                newName: "Institution");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Institution",
                table: "Clients",
                newName: "Institutuion");
        }
    }
}
