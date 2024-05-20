using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetWorldOficial.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(120)", maxLength: 120, nullable: false),
                    Price = table.Column<decimal>(type: "MONEY", nullable: false),
                    Image = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Company = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    CNPJ = table.Column<string>(type: "VARCHAR(11)", maxLength: 11, nullable: false),
                    Representative = table.Column<string>(type: "NVARCHAR(180)", maxLength: 180, nullable: false),
                    PhoneNumber = table.Column<string>(type: "VARCHAR(11)", maxLength: 11, nullable: false),
                    Street = table.Column<string>(type: "NVARCHAR(180)", maxLength: 180, nullable: false),
                    Number = table.Column<int>(type: "INT", nullable: false),
                    Neighborhood = table.Column<string>(type: "NVARCHAR(180)", maxLength: 180, nullable: false),
                    Complement = table.Column<string>(type: "NVARCHAR(120)", maxLength: 120, nullable: false),
                    City = table.Column<string>(type: "NVARCHAR(180)", maxLength: 180, nullable: false),
                    State = table.Column<string>(type: "NVARCHAR(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(180)", maxLength: 180, nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    Image = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    Price = table.Column<decimal>(type: "MONEY", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Supplier",
                        column: x => x.SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_Id",
                table: "Product",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_SupplierId",
                table: "Product",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_Id",
                table: "Service",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_Id",
                table: "Supplier",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "Supplier");
        }
    }
}
