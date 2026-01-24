using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AquaSolution.Data.Migrations
{
    public partial class add_table_Calculated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CalculatedId",
                schema: "KPI",
                table: "tbl_KPITasks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_KPITasks_CalculatedId",
                schema: "KPI",
                table: "tbl_KPITasks",
                column: "CalculatedId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_KPITasks_tbl_QuarterCalculates_CalculatedId",
                schema: "KPI",
                table: "tbl_KPITasks",
                column: "CalculatedId",
                principalSchema: "KPI",
                principalTable: "tbl_QuarterCalculates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_KPITasks_tbl_QuarterCalculates_CalculatedId",
                schema: "KPI",
                table: "tbl_KPITasks");

            migrationBuilder.DropIndex(
                name: "IX_tbl_KPITasks_CalculatedId",
                schema: "KPI",
                table: "tbl_KPITasks");

            migrationBuilder.DropColumn(
                name: "CalculatedId",
                schema: "KPI",
                table: "tbl_KPITasks");
        }
    }
}
