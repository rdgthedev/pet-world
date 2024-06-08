using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetWorldOficial.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_Animal_ServiceId",
                table: "Schedule");

            migrationBuilder.AlterColumn<string>(
                name: "Observation",
                table: "Schedule",
                type: "VARCHAR(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(255)");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_Animal_ServiceId",
                table: "Schedule",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_Animal_ServiceId",
                table: "Schedule");

            migrationBuilder.AlterColumn<string>(
                name: "Observation",
                table: "Schedule",
                type: "VARCHAR(255)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR(255)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_Animal_ServiceId",
                table: "Schedule",
                column: "AnimalId",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
