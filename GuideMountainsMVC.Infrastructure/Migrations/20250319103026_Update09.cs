using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GuideMountainsMVC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update09 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SkiPassTypeId",
                table: "SkiPasses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SkiPassTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PriceMultiplier = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkiPassTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReservationItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationId = table.Column<int>(type: "int", nullable: false),
                    AccommodationId = table.Column<int>(type: "int", nullable: true),
                    SkiPassId = table.Column<int>(type: "int", nullable: true),
                    EquipmentRentalId = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservationItems_Accommodations_AccommodationId",
                        column: x => x.AccommodationId,
                        principalTable: "Accommodations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ReservationItems_EquipmentRentals_EquipmentRentalId",
                        column: x => x.EquipmentRentalId,
                        principalTable: "EquipmentRentals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ReservationItems_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationItems_SkiPasses_SkiPassId",
                        column: x => x.SkiPassId,
                        principalTable: "SkiPasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "SkiPassTypes",
                columns: new[] { "Id", "Name", "PriceMultiplier" },
                values: new object[,]
                {
                    { 1, "Normalny", 1.0m },
                    { 2, "Ulgowy", 0.8m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SkiPasses_SkiPassTypeId",
                table: "SkiPasses",
                column: "SkiPassTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationItems_AccommodationId",
                table: "ReservationItems",
                column: "AccommodationId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationItems_EquipmentRentalId",
                table: "ReservationItems",
                column: "EquipmentRentalId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationItems_ReservationId",
                table: "ReservationItems",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationItems_SkiPassId",
                table: "ReservationItems",
                column: "SkiPassId");

            migrationBuilder.AddForeignKey(
                name: "FK_SkiPasses_SkiPassTypes_SkiPassTypeId",
                table: "SkiPasses",
                column: "SkiPassTypeId",
                principalTable: "SkiPassTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SkiPasses_SkiPassTypes_SkiPassTypeId",
                table: "SkiPasses");

            migrationBuilder.DropTable(
                name: "ReservationItems");

            migrationBuilder.DropTable(
                name: "SkiPassTypes");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_SkiPasses_SkiPassTypeId",
                table: "SkiPasses");

            migrationBuilder.DropColumn(
                name: "SkiPassTypeId",
                table: "SkiPasses");
        }
    }
}
