using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetWorldOficial.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovingUniquenessFromTheFKS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Schedule_AnimalId",
                table: "Schedule");

            migrationBuilder.DropIndex(
                name: "IX_Schedule_ServiceId",
                table: "Schedule");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_AnimalId",
                table: "Schedule",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_ServiceId",
                table: "Schedule",
                column: "ServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Schedule_AnimalId",
                table: "Schedule");

            migrationBuilder.DropIndex(
                name: "IX_Schedule_ServiceId",
                table: "Schedule");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_AnimalId",
                table: "Schedule",
                column: "AnimalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_ServiceId",
                table: "Schedule",
                column: "ServiceId",
                unique: true);
        }
    }
}
