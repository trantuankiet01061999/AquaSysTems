using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AquaSolution.Data.Migrations
{
    /// <inheritdoc />
    public partial class add_feat_lockSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_tbl_KPIMonthlyActuals_tbl_KPIMonthlyTargets_KPIMonthlyTargetId",
            //    schema: "KPI",
            //    table: "tbl_KPIMonthlyActuals");

            //migrationBuilder.RenameColumn(
            //    name: "KPIMonthlyTargetId",
            //    schema: "KPI",
            //    table: "tbl_KPIMonthlyActuals",
            //    newName: "KPITargetId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_tbl_KPIMonthlyActuals_KPIMonthlyTargetId",
            //    schema: "KPI",
            //    table: "tbl_KPIMonthlyActuals",
            //    newName: "IX_tbl_KPIMonthlyActuals_KPITargetId");

            migrationBuilder.CreateTable(
                name: "tbl_SystemLock",
                schema: "Admin",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsLocket = table.Column<bool>(type: "bit", nullable: false),
                    LockedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LockedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_SystemLock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_SystemLock_tbl_Pages_PageId",
                        column: x => x.PageId,
                        principalSchema: "Admin",
                        principalTable: "tbl_Pages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_SystemLock_tbl_Users_LockedBy",
                        column: x => x.LockedBy,
                        principalSchema: "Admin",
                        principalTable: "tbl_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_SystemLock_LockedBy",
                schema: "Admin",
                table: "tbl_SystemLock",
                column: "LockedBy");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_SystemLock_PageId",
                schema: "Admin",
                table: "tbl_SystemLock",
                column: "PageId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_tbl_KPIMonthlyActuals_tbl_KPIMonthlyTargets_KPITargetId",
            //    schema: "KPI",
            //    table: "tbl_KPIMonthlyActuals",
            //    column: "KPITargetId",
            //    principalSchema: "KPI",
            //    principalTable: "tbl_KPIMonthlyTargets",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_tbl_KPIMonthlyActuals_tbl_KPIMonthlyTargets_KPITargetId",
            //    schema: "KPI",
            //    table: "tbl_KPIMonthlyActuals");

            migrationBuilder.DropTable(
                name: "tbl_SystemLock",
                schema: "Admin");

            //migrationBuilder.RenameColumn(
            //    name: "KPITargetId",
            //    schema: "KPI",
            //    table: "tbl_KPIMonthlyActuals",
            //    newName: "KPIMonthlyTargetId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_tbl_KPIMonthlyActuals_KPITargetId",
            //    schema: "KPI",
            //    table: "tbl_KPIMonthlyActuals",
            //    newName: "IX_tbl_KPIMonthlyActuals_KPIMonthlyTargetId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_tbl_KPIMonthlyActuals_tbl_KPIMonthlyTargets_KPIMonthlyTargetId",
            //    schema: "KPI",
            //    table: "tbl_KPIMonthlyActuals",
            //    column: "KPIMonthlyTargetId",
            //    principalSchema: "KPI",
            //    principalTable: "tbl_KPIMonthlyTargets",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }
    }
}
