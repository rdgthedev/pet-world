using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetWorldOficial.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BirthDateRemovedFromTheAnimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Animal");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Animal",
                type: "DATETIME",
                nullable: true);
        }
    }
}
