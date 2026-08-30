using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Products.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addproductidtohistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                schema: "Products",
                table: "ProductPriceHistories",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductId",
                schema: "Products",
                table: "ProductPriceHistories");
        }
    }
}
