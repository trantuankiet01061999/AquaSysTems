using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AquaSolution.Data.Migrations
{
    /// <inheritdoc />
    public partial class add_table_Calculated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "QuarterCalculateId",
                schema: "KPI",
                table: "tbl_KPITasks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            //migrationBuilder.AddColumn<int>(
            //    name: "HalfYear",
            //    schema: "KPI",
            //    table: "tbl_KPIMonthlyTargets",
            //    type: "int",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "Quarter",
            //    schema: "KPI",
            //    table: "tbl_KPIMonthlyTargets",
            //    type: "int",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "HalfYear",
            //    schema: "KPI",
            //    table: "tbl_KPIMonthlyActuals",
            //    type: "int",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "Quarter",
            //    schema: "KPI",
            //    table: "tbl_KPIMonthlyActuals",
            //    type: "int",
            //    nullable: true);

            migrationBuilder.CreateTable(
                name: "tbl_QuarterCalculates",
                schema: "KPI",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuarterCalculated = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuarterCalculateType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_QuarterCalculates", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_KPITasks_QuarterCalculateId",
                schema: "KPI",
                table: "tbl_KPITasks",
                column: "QuarterCalculateId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_KPITasks_tbl_QuarterCalculates_QuarterCalculateId",
                schema: "KPI",
                table: "tbl_KPITasks",
                column: "QuarterCalculateId",
                principalSchema: "KPI",
                principalTable: "tbl_QuarterCalculates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_KPITasks_tbl_QuarterCalculates_QuarterCalculateId",
                schema: "KPI",
                table: "tbl_KPITasks");

            migrationBuilder.DropTable(
                name: "tbl_QuarterCalculates",
                schema: "KPI");

            migrationBuilder.DropIndex(
                name: "IX_tbl_KPITasks_QuarterCalculateId",
                schema: "KPI",
                table: "tbl_KPITasks");

            migrationBuilder.DropColumn(
                name: "QuarterCalculateId",
                schema: "KPI",
                table: "tbl_KPITasks");

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
