using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AquaSolution.Data.Migrations
{
    public partial class Update_table_KPI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Drop FK cũ
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_KPIApprovalTasks_tbl_KPIRequests_KPIRequestId",
                table: "tbl_KPIApprovalTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_KPITotalScores_tbl_KPIRequests_KPIRequestId",
                table: "tbl_KPITotalScores");

            // 2. Rename column KPIRequestId -> SubmitId trong bảng con (không tạo trùng cột)
            migrationBuilder.RenameColumn(
                name: "KPIRequestId",
                table: "tbl_KPIApprovalTasks",
                newName: "SubmitId");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_KPIApprovalTasks_KPIRequestId",
                table: "tbl_KPIApprovalTasks",
                newName: "IX_tbl_KPIApprovalTasks_SubmitId");

            migrationBuilder.RenameColumn(
                name: "KPIRequestId",
                table: "tbl_KPITotalScores",
                newName: "SubmitId");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_KPITotalScores_KPIRequestId",
                table: "tbl_KPITotalScores",
                newName: "IX_tbl_KPITotalScores_SubmitId");

            // 3. Thêm cột Title vào tbl_KPITotalScores
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "tbl_KPITotalScores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            // 4. Thêm FK mới trỏ tới Id của tbl_KPIRequests (không dùng SubmitId, tránh lỗi)
            migrationBuilder.AddForeignKey(
                name: "FK_tbl_KPIApprovalTasks_tbl_KPIRequests_SubmitId",
                table: "tbl_KPIApprovalTasks",
                column: "SubmitId",
                principalTable: "tbl_KPIRequests",
                principalColumn: "Id", // dùng Id để đảm bảo PK/candidate key
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_KPITotalScores_tbl_KPIRequests_SubmitId",
                table: "tbl_KPITotalScores",
                column: "SubmitId",
                principalTable: "tbl_KPIRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // 1. Drop new FK
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_KPIApprovalTasks_tbl_KPIRequests_SubmitId",
                table: "tbl_KPIApprovalTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_KPITotalScores_tbl_KPIRequests_SubmitId",
                table: "tbl_KPITotalScores");

            // 2. Drop Title column
            migrationBuilder.DropColumn(
                name: "Title",
                table: "tbl_KPITotalScores");

            // 3. Rename SubmitId -> KPIRequestId trong bảng con
            migrationBuilder.RenameColumn(
                name: "SubmitId",
                table: "tbl_KPIApprovalTasks",
                newName: "KPIRequestId");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_KPIApprovalTasks_SubmitId",
                table: "tbl_KPIApprovalTasks",
                newName: "IX_tbl_KPIApprovalTasks_KPIRequestId");

            migrationBuilder.RenameColumn(
                name: "SubmitId",
                table: "tbl_KPITotalScores",
                newName: "KPIRequestId");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_KPITotalScores_SubmitId",
                table: "tbl_KPITotalScores",
                newName: "IX_tbl_KPITotalScores_KPIRequestId");

            // 4. Re-add old FK
            migrationBuilder.AddForeignKey(
                name: "FK_tbl_KPIApprovalTasks_tbl_KPIRequests_KPIRequestId",
                table: "tbl_KPIApprovalTasks",
                column: "KPIRequestId",
                principalTable: "tbl_KPIRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_KPITotalScores_tbl_KPIRequests_KPIRequestId",
                table: "tbl_KPITotalScores",
                column: "KPIRequestId",
                principalTable: "tbl_KPIRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
