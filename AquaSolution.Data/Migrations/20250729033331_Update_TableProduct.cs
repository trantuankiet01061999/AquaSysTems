using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AquaSolution.Data.Migrations
{
    /// <inheritdoc />
    public partial class Update_TableProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "tbl_Products",
                type: "nvarchar(2400)",
                maxLength: 2400,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2400)",
                oldMaxLength: 2400);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "tbl_Products",
                type: "nvarchar(2400)",
                maxLength: 2400,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(2400)",
                oldMaxLength: 2400,
                oldNullable: true);
        }
    }
}
