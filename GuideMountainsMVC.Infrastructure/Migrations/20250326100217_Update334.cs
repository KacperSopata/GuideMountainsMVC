using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuideMountainsMVC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update334 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EquipmentName",
                table: "ReservationItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SkiPassTypeName",
                table: "ReservationItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EquipmentName",
                table: "ReservationItems");

            migrationBuilder.DropColumn(
                name: "SkiPassTypeName",
                table: "ReservationItems");
        }
    }
}
