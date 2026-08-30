using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Products.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class inheritancebaseentityforpricehistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "OldPrice",
                schema: "Products",
                table: "ProductPriceHistories",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "NewPrice",
                schema: "Products",
                table: "ProductPriceHistories",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                schema: "Products",
                table: "ProductPriceHistories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Products",
                table: "ProductPriceHistories",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                schema: "Products",
                table: "ProductPriceHistories",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "Products",
                table: "ProductPriceHistories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Products",
                table: "ProductPriceHistories");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "Products",
                table: "ProductPriceHistories");

            migrationBuilder.AlterColumn<decimal>(
                name: "OldPrice",
                schema: "Products",
                table: "ProductPriceHistories",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "NewPrice",
                schema: "Products",
                table: "ProductPriceHistories",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");
        }
    }
}
