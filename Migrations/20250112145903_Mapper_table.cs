using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prezenta_API.Migrations
{
    /// <inheritdoc />
    public partial class Mapper_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mappers",
                columns: table => new
                {
                    UserCode = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mappers", x => x.UserCode);
                });

            migrationBuilder.InsertData(
                table: "Mappers",
                columns: new[] { "UserCode", "UserId", "isActive" },
                values: new object[] { -1, -1, false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mappers");
        }
    }
}
