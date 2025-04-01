using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuideMountainsMVC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AccommodationEndDate",
                table: "ReservationItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccommodationGuests",
                table: "ReservationItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccommodationNights",
                table: "ReservationItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AccommodationStartDate",
                table: "ReservationItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EquipmentDays",
                table: "ReservationItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EquipmentQuantity",
                table: "ReservationItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SkiPassDays",
                table: "ReservationItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SkiPassQuantity",
                table: "ReservationItems",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccommodationEndDate",
                table: "ReservationItems");

            migrationBuilder.DropColumn(
                name: "AccommodationGuests",
                table: "ReservationItems");

            migrationBuilder.DropColumn(
                name: "AccommodationNights",
                table: "ReservationItems");

            migrationBuilder.DropColumn(
                name: "AccommodationStartDate",
                table: "ReservationItems");

            migrationBuilder.DropColumn(
                name: "EquipmentDays",
                table: "ReservationItems");

            migrationBuilder.DropColumn(
                name: "EquipmentQuantity",
                table: "ReservationItems");

            migrationBuilder.DropColumn(
                name: "SkiPassDays",
                table: "ReservationItems");

            migrationBuilder.DropColumn(
                name: "SkiPassQuantity",
                table: "ReservationItems");
        }
    }
}
