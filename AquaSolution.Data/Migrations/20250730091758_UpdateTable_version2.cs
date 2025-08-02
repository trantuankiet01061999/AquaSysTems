using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AquaSolution.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTable_version2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalSourceType",
                table: "tbl_Inventory");

            migrationBuilder.RenameColumn(
                name: "CreatedData",
                table: "tbl_WarehouseImports",
                newName: "CreatedDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "tbl_WarehouseImports",
                newName: "CreatedData");

            migrationBuilder.AddColumn<int>(
                name: "AdditionalSourceType",
                table: "tbl_Inventory",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
