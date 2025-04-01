using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuideMountainsMVC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update333 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "EquipmentRentals");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "EquipmentRentals",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
