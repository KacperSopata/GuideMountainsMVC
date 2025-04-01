using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuideMountainsMVC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update54 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "PriceMultiplier",
                table: "SkiPassTypes",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                table: "SkiPassTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "PriceMultiplier",
                value: 1.0);

            migrationBuilder.UpdateData(
                table: "SkiPassTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "PriceMultiplier",
                value: 0.80000000000000004);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PriceMultiplier",
                table: "SkiPassTypes",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.UpdateData(
                table: "SkiPassTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "PriceMultiplier",
                value: 1.0m);

            migrationBuilder.UpdateData(
                table: "SkiPassTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "PriceMultiplier",
                value: 0.8m);
        }
    }
}
