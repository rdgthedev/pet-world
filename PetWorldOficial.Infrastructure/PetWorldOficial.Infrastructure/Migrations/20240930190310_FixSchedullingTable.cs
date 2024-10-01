using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetWorldOficial.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixSchedullingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Schedulling",
                type: "NVARCHAR(120)",
                maxLength: 120,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(20)",
                oldMaxLength: 20);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Schedulling",
                type: "NVARCHAR(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(120)",
                oldMaxLength: 120);
        }
    }
}
