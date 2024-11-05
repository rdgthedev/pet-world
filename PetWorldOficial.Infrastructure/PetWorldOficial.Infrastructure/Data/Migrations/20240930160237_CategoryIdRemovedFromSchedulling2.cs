using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetWorldOficial.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CategoryIdRemovedFromSchedulling2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedulling_Category_CategoryId",
                table: "Schedulling");

            migrationBuilder.DropIndex(
                name: "IX_Schedulling_CategoryId",
                table: "Schedulling");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Schedulling");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Schedulling",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schedulling_CategoryId",
                table: "Schedulling",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedulling_Category_CategoryId",
                table: "Schedulling",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }
    }
}
