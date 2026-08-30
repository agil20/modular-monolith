using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Modules.Products.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixFluentApiFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPriceHistories_Products_Id",
                schema: "Products",
                table: "ProductPriceHistories");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "Products",
                table: "ProductPriceHistories",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_ProductPriceHistories_ProductId",
                schema: "Products",
                table: "ProductPriceHistories",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPriceHistories_Products_ProductId",
                schema: "Products",
                table: "ProductPriceHistories",
                column: "ProductId",
                principalSchema: "Products",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPriceHistories_Products_ProductId",
                schema: "Products",
                table: "ProductPriceHistories");

            migrationBuilder.DropIndex(
                name: "IX_ProductPriceHistories_ProductId",
                schema: "Products",
                table: "ProductPriceHistories");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "Products",
                table: "ProductPriceHistories",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPriceHistories_Products_Id",
                schema: "Products",
                table: "ProductPriceHistories",
                column: "Id",
                principalSchema: "Products",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
