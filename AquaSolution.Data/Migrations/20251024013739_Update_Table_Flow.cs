using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AquaSolution.Data.Migrations
{
    /// <inheritdoc />
    public partial class Update_Table_Flow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "System",
                table: "tbl_ApprovalFlows");

            migrationBuilder.RenameColumn(
                name: "UserApproveId",
                table: "tbl_ApprovalFlows",
                newName: "DecisionMaker");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DecisionMaker",
                table: "tbl_ApprovalFlows",
                newName: "UserApproveId");

            migrationBuilder.AddColumn<string>(
                name: "System",
                table: "tbl_ApprovalFlows",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
