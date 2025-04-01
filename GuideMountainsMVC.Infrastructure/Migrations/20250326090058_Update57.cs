using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuideMountainsMVC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update57 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "SkiPasses");

            migrationBuilder.AddColumn<string>(
                name: "MountainPlaceName",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MountainPlaceName",
                table: "Reservations");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SkiPasses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
