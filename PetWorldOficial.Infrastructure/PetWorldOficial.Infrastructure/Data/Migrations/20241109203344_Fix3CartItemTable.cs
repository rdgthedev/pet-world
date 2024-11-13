using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetWorldOficial.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fix3CartItemTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CartItem_ProductId",
                table: "CartItem");

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_ProductId",
                table: "CartItem",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CartItem_ProductId",
                table: "CartItem");

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_ProductId",
                table: "CartItem",
                column: "ProductId",
                unique: true);
        }
    }
}
