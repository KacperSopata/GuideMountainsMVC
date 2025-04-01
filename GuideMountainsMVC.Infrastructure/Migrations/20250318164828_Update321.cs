using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuideMountainsMVC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update321 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentRentals_CategoryEquipmentRental_CategoryEquipmentRentalId",
                table: "EquipmentRentals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryEquipmentRental",
                table: "CategoryEquipmentRental");

            migrationBuilder.RenameTable(
                name: "CategoryEquipmentRental",
                newName: "CategoryEquipmentRentals");

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "EquipmentRentals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryEquipmentRentals",
                table: "CategoryEquipmentRentals",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentRentals_CategoryEquipmentRentals_CategoryEquipmentRentalId",
                table: "EquipmentRentals",
                column: "CategoryEquipmentRentalId",
                principalTable: "CategoryEquipmentRentals",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentRentals_CategoryEquipmentRentals_CategoryEquipmentRentalId",
                table: "EquipmentRentals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryEquipmentRentals",
                table: "CategoryEquipmentRentals");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "EquipmentRentals");

            migrationBuilder.RenameTable(
                name: "CategoryEquipmentRentals",
                newName: "CategoryEquipmentRental");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryEquipmentRental",
                table: "CategoryEquipmentRental",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentRentals_CategoryEquipmentRental_CategoryEquipmentRentalId",
                table: "EquipmentRentals",
                column: "CategoryEquipmentRentalId",
                principalTable: "CategoryEquipmentRental",
                principalColumn: "Id");
        }
    }
}
