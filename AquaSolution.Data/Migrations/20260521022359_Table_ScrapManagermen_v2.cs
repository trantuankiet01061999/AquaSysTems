using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AquaSolution.Data.Migrations
{
    /// <inheritdoc />
    public partial class Table_ScrapManagermen_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ScrapManagement");

            migrationBuilder.RenameTable(
                name: "tbl_Weights",
                newName: "tbl_Weights",
                newSchema: "ScrapManagement");

            migrationBuilder.RenameTable(
                name: "tbl_RequestApprovals",
                newName: "tbl_RequestApprovals",
                newSchema: "ScrapManagement");

            migrationBuilder.RenameTable(
                name: "tbl_Materials",
                newName: "tbl_Materials",
                newSchema: "ScrapManagement");

            migrationBuilder.RenameTable(
                name: "tbl_HistoryScraps",
                newName: "tbl_HistoryScraps",
                newSchema: "ScrapManagement");

            migrationBuilder.RenameTable(
                name: "tbl_HistoryScrapDetails",
                newName: "tbl_HistoryScrapDetails",
                newSchema: "ScrapManagement");

            migrationBuilder.AlterColumn<decimal>(
                name: "WeightValue",
                schema: "ScrapManagement",
                table: "tbl_Weights",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                schema: "ScrapManagement",
                table: "tbl_RequestApprovals",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Plant",
                schema: "ScrapManagement",
                table: "tbl_Materials",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                schema: "ScrapManagement",
                table: "tbl_HistoryScraps",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Weight",
                schema: "ScrapManagement",
                table: "tbl_HistoryScrapDetails",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWeight",
                schema: "ScrapManagement",
                table: "tbl_HistoryScrapDetails",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                schema: "ScrapManagement",
                table: "tbl_HistoryScrapDetails",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Plant",
                schema: "ScrapManagement",
                table: "tbl_HistoryScrapDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Weights_CreatedBy",
                schema: "ScrapManagement",
                table: "tbl_Weights",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Weights_MaterialId",
                schema: "ScrapManagement",
                table: "tbl_Weights",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_RequestApprovals_ActionBy",
                schema: "ScrapManagement",
                table: "tbl_RequestApprovals",
                column: "ActionBy");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_RequestApprovals_DecisionMaker",
                schema: "ScrapManagement",
                table: "tbl_RequestApprovals",
                column: "DecisionMaker");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_RequestApprovals_HistoryScrapId",
                schema: "ScrapManagement",
                table: "tbl_RequestApprovals",
                column: "HistoryScrapId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_HistoryScraps_CreatedBy",
                schema: "ScrapManagement",
                table: "tbl_HistoryScraps",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_HistoryScraps_LastActionBy",
                schema: "ScrapManagement",
                table: "tbl_HistoryScraps",
                column: "LastActionBy");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_HistoryScrapDetails_MaterialId",
                schema: "ScrapManagement",
                table: "tbl_HistoryScrapDetails",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_HistoryScrapDetails_ScrapHistoryId",
                schema: "ScrapManagement",
                table: "tbl_HistoryScrapDetails",
                column: "ScrapHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_HistoryScrapDetails_tbl_HistoryScraps_ScrapHistoryId",
                schema: "ScrapManagement",
                table: "tbl_HistoryScrapDetails",
                column: "ScrapHistoryId",
                principalSchema: "ScrapManagement",
                principalTable: "tbl_HistoryScraps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_HistoryScrapDetails_tbl_Materials_MaterialId",
                schema: "ScrapManagement",
                table: "tbl_HistoryScrapDetails",
                column: "MaterialId",
                principalSchema: "ScrapManagement",
                principalTable: "tbl_Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_HistoryScraps_tbl_Users_CreatedBy",
                schema: "ScrapManagement",
                table: "tbl_HistoryScraps",
                column: "CreatedBy",
                principalSchema: "Admin",
                principalTable: "tbl_Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_HistoryScraps_tbl_Users_LastActionBy",
                schema: "ScrapManagement",
                table: "tbl_HistoryScraps",
                column: "LastActionBy",
                principalSchema: "Admin",
                principalTable: "tbl_Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_RequestApprovals_tbl_HistoryScraps_HistoryScrapId",
                schema: "ScrapManagement",
                table: "tbl_RequestApprovals",
                column: "HistoryScrapId",
                principalSchema: "ScrapManagement",
                principalTable: "tbl_HistoryScraps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_RequestApprovals_tbl_Users_ActionBy",
                schema: "ScrapManagement",
                table: "tbl_RequestApprovals",
                column: "ActionBy",
                principalSchema: "Admin",
                principalTable: "tbl_Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_RequestApprovals_tbl_Users_DecisionMaker",
                schema: "ScrapManagement",
                table: "tbl_RequestApprovals",
                column: "DecisionMaker",
                principalSchema: "Admin",
                principalTable: "tbl_Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Weights_tbl_Materials_MaterialId",
                schema: "ScrapManagement",
                table: "tbl_Weights",
                column: "MaterialId",
                principalSchema: "ScrapManagement",
                principalTable: "tbl_Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Weights_tbl_Users_CreatedBy",
                schema: "ScrapManagement",
                table: "tbl_Weights",
                column: "CreatedBy",
                principalSchema: "Admin",
                principalTable: "tbl_Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_HistoryScrapDetails_tbl_HistoryScraps_ScrapHistoryId",
                schema: "ScrapManagement",
                table: "tbl_HistoryScrapDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_HistoryScrapDetails_tbl_Materials_MaterialId",
                schema: "ScrapManagement",
                table: "tbl_HistoryScrapDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_HistoryScraps_tbl_Users_CreatedBy",
                schema: "ScrapManagement",
                table: "tbl_HistoryScraps");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_HistoryScraps_tbl_Users_LastActionBy",
                schema: "ScrapManagement",
                table: "tbl_HistoryScraps");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_RequestApprovals_tbl_HistoryScraps_HistoryScrapId",
                schema: "ScrapManagement",
                table: "tbl_RequestApprovals");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_RequestApprovals_tbl_Users_ActionBy",
                schema: "ScrapManagement",
                table: "tbl_RequestApprovals");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_RequestApprovals_tbl_Users_DecisionMaker",
                schema: "ScrapManagement",
                table: "tbl_RequestApprovals");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Weights_tbl_Materials_MaterialId",
                schema: "ScrapManagement",
                table: "tbl_Weights");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Weights_tbl_Users_CreatedBy",
                schema: "ScrapManagement",
                table: "tbl_Weights");

            migrationBuilder.DropIndex(
                name: "IX_tbl_Weights_CreatedBy",
                schema: "ScrapManagement",
                table: "tbl_Weights");

            migrationBuilder.DropIndex(
                name: "IX_tbl_Weights_MaterialId",
                schema: "ScrapManagement",
                table: "tbl_Weights");

            migrationBuilder.DropIndex(
                name: "IX_tbl_RequestApprovals_ActionBy",
                schema: "ScrapManagement",
                table: "tbl_RequestApprovals");

            migrationBuilder.DropIndex(
                name: "IX_tbl_RequestApprovals_DecisionMaker",
                schema: "ScrapManagement",
                table: "tbl_RequestApprovals");

            migrationBuilder.DropIndex(
                name: "IX_tbl_RequestApprovals_HistoryScrapId",
                schema: "ScrapManagement",
                table: "tbl_RequestApprovals");

            migrationBuilder.DropIndex(
                name: "IX_tbl_HistoryScraps_CreatedBy",
                schema: "ScrapManagement",
                table: "tbl_HistoryScraps");

            migrationBuilder.DropIndex(
                name: "IX_tbl_HistoryScraps_LastActionBy",
                schema: "ScrapManagement",
                table: "tbl_HistoryScraps");

            migrationBuilder.DropIndex(
                name: "IX_tbl_HistoryScrapDetails_MaterialId",
                schema: "ScrapManagement",
                table: "tbl_HistoryScrapDetails");

            migrationBuilder.DropIndex(
                name: "IX_tbl_HistoryScrapDetails_ScrapHistoryId",
                schema: "ScrapManagement",
                table: "tbl_HistoryScrapDetails");

            migrationBuilder.RenameTable(
                name: "tbl_Weights",
                schema: "ScrapManagement",
                newName: "tbl_Weights");

            migrationBuilder.RenameTable(
                name: "tbl_RequestApprovals",
                schema: "ScrapManagement",
                newName: "tbl_RequestApprovals");

            migrationBuilder.RenameTable(
                name: "tbl_Materials",
                schema: "ScrapManagement",
                newName: "tbl_Materials");

            migrationBuilder.RenameTable(
                name: "tbl_HistoryScraps",
                schema: "ScrapManagement",
                newName: "tbl_HistoryScraps");

            migrationBuilder.RenameTable(
                name: "tbl_HistoryScrapDetails",
                schema: "ScrapManagement",
                newName: "tbl_HistoryScrapDetails");

            migrationBuilder.AlterColumn<decimal>(
                name: "WeightValue",
                table: "tbl_Weights",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "tbl_RequestApprovals",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Plant",
                table: "tbl_Materials",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "tbl_HistoryScraps",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Weight",
                table: "tbl_HistoryScrapDetails",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWeight",
                table: "tbl_HistoryScrapDetails",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                table: "tbl_HistoryScrapDetails",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "Plant",
                table: "tbl_HistoryScrapDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
