using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetWorldOficial.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animal_User_UserId",
                table: "Animal");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropIndex(
                name: "IX_Supplier_Id",
                table: "Supplier");

            migrationBuilder.DropIndex(
                name: "IX_Service_Id",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Service_Name",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "Company",
                table: "Supplier");

            migrationBuilder.DropColumn(
                name: "Representative",
                table: "Supplier");

            migrationBuilder.DropColumn(
                name: "Race",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "Species",
                table: "Animal");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Supplier",
                newName: "CellPhone");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Service",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Animal",
                newName: "RaceId");

            migrationBuilder.RenameIndex(
                name: "IX_Animal_UserId",
                table: "Animal",
                newName: "IX_Animal_RaceId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Supplier",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "Supplier",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Supplier",
                type: "NVARCHAR(140)",
                maxLength: 140,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Supplier",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Service",
                type: "DECIMAL(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "MONEY");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Service",
                type: "NVARCHAR(120)",
                maxLength: 120,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Service",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Service",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DurationInMinutes",
                table: "Service",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "Service",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Service",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Product",
                type: "VARCHAR(120)",
                maxLength: 120,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(180)",
                oldMaxLength: 180);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Product",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "Product",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "Gender",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Complement",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetRoles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "AspNetRoles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Animal",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Animal",
                type: "DATE",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Animal",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Animal",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "Animal",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Animal",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    ExpiresDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "DECIMAL(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cart_User_ClientId",
                        column: x => x.ClientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "NVARCHAR(120)", maxLength: 120, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "NVARCHAR(10)", maxLength: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "DECIMAL(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_AspNetUsers_ClientId",
                        column: x => x.ClientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "Stock",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "INT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stock_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedulling",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimalId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "NVARCHAR(20)", maxLength: 20, nullable: false),
                    Date = table.Column<DateTime>(type: "DATE", nullable: false),
                    Time = table.Column<TimeSpan>(type: "TIME", nullable: false),
                    Observation = table.Column<string>(type: "NVARCHAR", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedulling", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedulling_Animal_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedulling_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schedulling_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schedulling_User_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CartItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "INT", nullable: false),
                    Price = table.Column<decimal>(type: "DECIMAL(10,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "DECIMAL(10,2)", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItem_Cart_CartId",
                        column: x => x.CartId,
                        principalTable: "Cart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItem_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItem_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Service_Category_CategoryId",
                table: "Service",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_Id",
                table: "Service",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Service_Name",
                table: "Service",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Animal_CategoryId",
                table: "Animal",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Animal_Name",
                table: "Animal",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Animal_OwnerId",
                table: "Animal",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_ClientId",
                table: "Cart",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_CartId",
                table: "CartItem",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_Id",
                table: "CartItem",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_OrderId",
                table: "CartItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_ProductId",
                table: "CartItem",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_ClientId",
                table: "Order",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Code",
                table: "Order",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Id",
                table: "Order",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Race_Id",
                table: "Race",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Schedulling_AnimalId",
                table: "Schedulling",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedulling_CategoryId",
                table: "Schedulling",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedulling_EmployeeId",
                table: "Schedulling",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedulling_Id",
                table: "Schedulling",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Schedulling_ServiceId",
                table: "Schedulling",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_Id",
                table: "Stock",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_ProductId",
                table: "Stock",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stock_ProductId1",
                table: "Stock",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_Category_CategoryId",
                table: "Animal",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_Race_RaceId",
                table: "Animal",
                column: "RaceId",
                principalTable: "Race",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_User_UserId",
                table: "Animal",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_CategoryId",
                table: "Product",
                column: "SupplierId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Category_CategoryId",
                table: "Service",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animal_Category_CategoryId",
                table: "Animal");

            migrationBuilder.DropForeignKey(
                name: "FK_Animal_Race_RaceId",
                table: "Animal");

            migrationBuilder.DropForeignKey(
                name: "FK_Animal_User_UserId",
                table: "Animal");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_CategoryId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_Category_CategoryId",
                table: "Service");

            migrationBuilder.DropTable(
                name: "CartItem");

            migrationBuilder.DropTable(
                name: "Race");

            migrationBuilder.DropTable(
                name: "Schedulling");

            migrationBuilder.DropTable(
                name: "Stock");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Service_Category_CategoryId",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Service_Id",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Service_Name",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Animal_CategoryId",
                table: "Animal");

            migrationBuilder.DropIndex(
                name: "IX_Animal_Name",
                table: "Animal");

            migrationBuilder.DropIndex(
                name: "IX_Animal_OwnerId",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Supplier");

            migrationBuilder.DropColumn(
                name: "LastUpdatedAt",
                table: "Supplier");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Supplier");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Supplier");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "DurationInMinutes",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "LastUpdatedAt",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "LastUpdatedAt",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastUpdatedAt",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "LastUpdatedAt",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "LastUpdatedAt",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Animal");

            migrationBuilder.RenameColumn(
                name: "CellPhone",
                table: "Supplier",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Service",
                newName: "Image");

            migrationBuilder.RenameColumn(
                name: "RaceId",
                table: "Animal",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Animal_RaceId",
                table: "Animal",
                newName: "IX_Animal_UserId");

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "Supplier",
                type: "NVARCHAR(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Representative",
                table: "Supplier",
                type: "NVARCHAR(180)",
                maxLength: 180,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Service",
                type: "MONEY",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(10,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Service",
                type: "NVARCHAR(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(120)",
                oldMaxLength: 120);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Product",
                type: "VARCHAR(180)",
                maxLength: 180,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(120)",
                oldMaxLength: 120);

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Complement",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Animal",
                type: "NVARCHAR(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "Race",
                table: "Animal",
                type: "NVARCHAR(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Species",
                table: "Animal",
                type: "NVARCHAR(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimalId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Observation = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Time = table.Column<TimeSpan>(type: "TIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedule_Animal_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedule_Animal_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_Id",
                table: "Supplier",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Service_Id",
                table: "Service",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Service_Name",
                table: "Service",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_AnimalId",
                table: "Schedule",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_Id",
                table: "Schedule",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_ServiceId",
                table: "Schedule",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_User_UserId",
                table: "Animal",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
