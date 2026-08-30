using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Products.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedProductpricehistories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVip",
                schema: "Products",
                table: "Products",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ProductPriceHistories",
                schema: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    OldPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    NewPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    ChangedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPriceHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductPriceHistories_Products_Id",
                        column: x => x.Id,
                        principalSchema: "Products",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 101,
                column: "IsVip",
                value: false);

            migrationBuilder.UpdateData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 102,
                column: "IsVip",
                value: false);

            migrationBuilder.UpdateData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 103,
                column: "IsVip",
                value: false);

            migrationBuilder.UpdateData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 104,
                column: "IsVip",
                value: false);

            migrationBuilder.UpdateData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 105,
                column: "IsVip",
                value: false);

            migrationBuilder.UpdateData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 106,
                column: "IsVip",
                value: false);

            migrationBuilder.UpdateData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 107,
                column: "IsVip",
                value: false);

            migrationBuilder.UpdateData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 108,
                column: "IsVip",
                value: false);

            migrationBuilder.UpdateData(
                schema: "Products",
                table: "Products",
                keyColumn: "Id",
                keyValue: 109,
                column: "IsVip",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductPriceHistories",
                schema: "Products");

            migrationBuilder.DropColumn(
                name: "IsVip",
                schema: "Products",
                table: "Products");
        }
    }
}
