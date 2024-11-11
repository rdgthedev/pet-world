using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetWorldOficial.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRaceTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animal_Race_RaceId",
                table: "Animal");

            migrationBuilder.DropTable(
                name: "Race");

            migrationBuilder.DropIndex(
                name: "IX_Animal_RaceId",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "RaceId",
                table: "Animal");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RaceId",
                table: "Animal",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Race",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Race", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animal_RaceId",
                table: "Animal",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Race_Id",
                table: "Race",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_Race_RaceId",
                table: "Animal",
                column: "RaceId",
                principalTable: "Race",
                principalColumn: "Id");
        }
    }
}
