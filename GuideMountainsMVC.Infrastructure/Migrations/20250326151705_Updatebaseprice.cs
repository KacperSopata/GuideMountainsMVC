using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuideMountainsMVC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Updatebaseprice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PricePerDay",
                table: "SkiPasses",
                newName: "BasePrice");

            migrationBuilder.AddColumn<double>(
                name: "PricePerDay",
                table: "EquipmentRentals",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PricePerDay",
                table: "EquipmentRentals");

            migrationBuilder.RenameColumn(
                name: "BasePrice",
                table: "SkiPasses",
                newName: "PricePerDay");
        }
    }
}
