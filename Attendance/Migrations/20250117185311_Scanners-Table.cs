using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceAPI.Migrations
{
    /// <inheritdoc />
    public partial class ScannersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Scanners",
                columns: table => new
                {
                    ScannerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScannerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scanners", x => x.ScannerId);
                });

            migrationBuilder.InsertData(
                table: "Scanners",
                columns: new[] { "ScannerId", "ScannerName", "isActive" },
                values: new object[] { -1, "ScannerName", false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Scanners");
        }
    }
}
