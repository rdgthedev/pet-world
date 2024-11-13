using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetWorldOficial.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRaceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animal_Race_RaceId",
                table: "Animal");

            migrationBuilder.AlterColumn<int>(
                name: "RaceId",
                table: "Animal",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Race",
                table: "Animal",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_Race_RaceId",
                table: "Animal",
                column: "RaceId",
                principalTable: "Race",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animal_Race_RaceId",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "Race",
                table: "Animal");

            migrationBuilder.AlterColumn<int>(
                name: "RaceId",
                table: "Animal",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_Race_RaceId",
                table: "Animal",
                column: "RaceId",
                principalTable: "Race",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
