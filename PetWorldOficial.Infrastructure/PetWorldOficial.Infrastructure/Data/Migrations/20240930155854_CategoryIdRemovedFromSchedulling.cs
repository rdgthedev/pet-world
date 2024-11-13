using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetWorldOficial.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CategoryIdRemovedFromSchedulling : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedulling_Category_CategoryId",
                table: "Schedulling");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Schedulling",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedulling_Category_CategoryId",
                table: "Schedulling",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedulling_Category_CategoryId",
                table: "Schedulling");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Schedulling",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedulling_Category_CategoryId",
                table: "Schedulling",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
