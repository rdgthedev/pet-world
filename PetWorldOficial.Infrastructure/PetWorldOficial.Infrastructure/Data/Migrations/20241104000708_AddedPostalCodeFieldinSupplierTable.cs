using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetWorldOficial.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedPostalCodeFieldinSupplierTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Supplier",
                type: "NVARCHAR(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(140)",
                oldMaxLength: 140);

            migrationBuilder.AlterColumn<string>(
                name: "Complement",
                table: "Supplier",
                type: "NVARCHAR(128)",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(120)",
                oldMaxLength: 120);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Supplier",
                type: "NVARCHAR(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Supplier",
                type: "NVARCHAR(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Supplier");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Supplier");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Supplier",
                type: "NVARCHAR(140)",
                maxLength: 140,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "Complement",
                table: "Supplier",
                type: "NVARCHAR(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "NVARCHAR(128)",
                oldMaxLength: 128,
                oldNullable: true);
        }
    }
}
