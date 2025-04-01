using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuideMountainsMVC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceToSkiPass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PricePerDay",
                table: "SkiPasses",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PricePerDay",
                table: "SkiPasses");
        }
    }
}
