using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AquaSolution.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTable_version1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                table: "tbl_Inventory",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateTable(
                name: "tbl_SysTemHistorys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HistoryFlow = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_SysTemHistorys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_WarehouseImports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedData = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_WarehouseImports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_WarehouseImportDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseImportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateManufacture = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    ProductType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_WarehouseImportDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_WarehouseImportDetails_tbl_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "tbl_Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_WarehouseImportDetails_tbl_WarehouseImports_WarehouseImportId",
                        column: x => x.WarehouseImportId,
                        principalTable: "tbl_WarehouseImports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_WarehouseImportDetails_ProductId",
                table: "tbl_WarehouseImportDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_WarehouseImportDetails_WarehouseImportId",
                table: "tbl_WarehouseImportDetails",
                column: "WarehouseImportId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_SysTemHistorys");

            migrationBuilder.DropTable(
                name: "tbl_WarehouseImportDetails");

            migrationBuilder.DropTable(
                name: "tbl_WarehouseImports");

            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                table: "tbl_Inventory",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");
        }
    }
}
