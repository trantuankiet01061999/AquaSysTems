using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AquaSolution.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HalfYear",
                schema: "KPI",
                table: "tbl_KPIMonthlyTargets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quarter",
                schema: "KPI",
                table: "tbl_KPIMonthlyTargets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HalfYear",
                schema: "KPI",
                table: "tbl_KPIMonthlyActuals",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quarter",
                schema: "KPI",
                table: "tbl_KPIMonthlyActuals",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HalfYear",
                schema: "KPI",
                table: "tbl_KPIMonthlyTargets");

            migrationBuilder.DropColumn(
                name: "Quarter",
                schema: "KPI",
                table: "tbl_KPIMonthlyTargets");

            migrationBuilder.DropColumn(
                name: "HalfYear",
                schema: "KPI",
                table: "tbl_KPIMonthlyActuals");

            migrationBuilder.DropColumn(
                name: "Quarter",
                schema: "KPI",
                table: "tbl_KPIMonthlyActuals");
        }
    }
}
