using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AquaSolution.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTable_ReportInventory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_ReportInventory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ReportInventory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_ReportInventory_tbl_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "tbl_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ReportInventoryDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReportInventoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BeginningInventory = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NewInbound = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ConsumPosition = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalStock = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ReportInventoryDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_ReportInventoryDetail_tbl_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "tbl_Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_ReportInventoryDetail_tbl_ReportInventory_ReportInventoryId",
                        column: x => x.ReportInventoryId,
                        principalTable: "tbl_ReportInventory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ReportInventory_CreatedBy",
                table: "tbl_ReportInventory",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ReportInventoryDetail_ProductId",
                table: "tbl_ReportInventoryDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ReportInventoryDetail_ReportInventoryId",
                table: "tbl_ReportInventoryDetail",
                column: "ReportInventoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_ReportInventoryDetail");

            migrationBuilder.DropTable(
                name: "tbl_ReportInventory");
        }
    }
}
