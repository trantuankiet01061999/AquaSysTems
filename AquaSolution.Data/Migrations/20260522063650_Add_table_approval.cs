using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AquaSolution.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_table_approval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                schema: "ScrapManagement",
                table: "tbl_HistoryScraps",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "FactoryId",
                schema: "ScrapManagement",
                table: "tbl_HistoryScraps",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_tbl_HistoryScraps_DepartmentId",
                schema: "ScrapManagement",
                table: "tbl_HistoryScraps",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_HistoryScraps_FactoryId",
                schema: "ScrapManagement",
                table: "tbl_HistoryScraps",
                column: "FactoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_HistoryScraps_tbl_Departments_DepartmentId",
                schema: "ScrapManagement",
                table: "tbl_HistoryScraps",
                column: "DepartmentId",
                principalSchema: "Admin",
                principalTable: "tbl_Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_HistoryScraps_tbl_Factorys_FactoryId",
                schema: "ScrapManagement",
                table: "tbl_HistoryScraps",
                column: "FactoryId",
                principalSchema: "Admin",
                principalTable: "tbl_Factorys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_HistoryScraps_tbl_Departments_DepartmentId",
                schema: "ScrapManagement",
                table: "tbl_HistoryScraps");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_HistoryScraps_tbl_Factorys_FactoryId",
                schema: "ScrapManagement",
                table: "tbl_HistoryScraps");

            migrationBuilder.DropIndex(
                name: "IX_tbl_HistoryScraps_DepartmentId",
                schema: "ScrapManagement",
                table: "tbl_HistoryScraps");

            migrationBuilder.DropIndex(
                name: "IX_tbl_HistoryScraps_FactoryId",
                schema: "ScrapManagement",
                table: "tbl_HistoryScraps");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                schema: "ScrapManagement",
                table: "tbl_HistoryScraps");

            migrationBuilder.DropColumn(
                name: "FactoryId",
                schema: "ScrapManagement",
                table: "tbl_HistoryScraps");
        }
    }
}
