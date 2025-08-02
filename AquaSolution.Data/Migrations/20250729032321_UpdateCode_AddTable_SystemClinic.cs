using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AquaSolution.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCode_AddTable_SystemClinic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "UserApproveId",
                table: "tbl_ApprovalFlows",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "ApprovalSettingType",
                table: "tbl_ApprovalFlows",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateTable(
                name: "tbl_Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(2400)", maxLength: 2400, nullable: false),
                    ProductType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsHide = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_RequestClinics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkDayUserRequestId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserRequestName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurposeType = table.Column<int>(type: "int", nullable: false),
                    ManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovalBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RejectDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RejectBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SuccesDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PharmacyManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Note = table.Column<string>(type: "nvarchar(2400)", maxLength: 2400, nullable: true),
                    HistoryReuqest = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_RequestClinics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_RequestClinics_tbl_Users_ApprovalBy",
                        column: x => x.ApprovalBy,
                        principalTable: "tbl_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_RequestClinics_tbl_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "tbl_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_RequestClinics_tbl_Users_PharmacyManagerId",
                        column: x => x.PharmacyManagerId,
                        principalTable: "tbl_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_RequestClinics_tbl_Users_UserRequestId",
                        column: x => x.UserRequestId,
                        principalTable: "tbl_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Inventory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ManufacturingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    expired = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Inventory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Inventory_tbl_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "tbl_Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_MedicalSuply",
                columns: table => new
                {
                    ProducId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_MedicalSuply", x => x.ProducId);
                    table.ForeignKey(
                        name: "FK_tbl_MedicalSuply_tbl_Products_ProducId",
                        column: x => x.ProducId,
                        principalTable: "tbl_Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Medicines",
                columns: table => new
                {
                    ProducId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Medicines", x => x.ProducId);
                    table.ForeignKey(
                        name: "FK_tbl_Medicines_tbl_Products_ProducId",
                        column: x => x.ProducId,
                        principalTable: "tbl_Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ApprovalFlows_PositionId",
                table: "tbl_ApprovalFlows",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Inventory_ProductId",
                table: "tbl_Inventory",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_RequestClinics_ApprovalBy",
                table: "tbl_RequestClinics",
                column: "ApprovalBy");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_RequestClinics_CreatedBy",
                table: "tbl_RequestClinics",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_RequestClinics_PharmacyManagerId",
                table: "tbl_RequestClinics",
                column: "PharmacyManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_RequestClinics_UserRequestId",
                table: "tbl_RequestClinics",
                column: "UserRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_ApprovalFlows_tbl_Positions_PositionId",
                table: "tbl_ApprovalFlows",
                column: "PositionId",
                principalTable: "tbl_Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_ApprovalFlows_tbl_Positions_PositionId",
                table: "tbl_ApprovalFlows");

            migrationBuilder.DropTable(
                name: "tbl_Inventory");

            migrationBuilder.DropTable(
                name: "tbl_MedicalSuply");

            migrationBuilder.DropTable(
                name: "tbl_Medicines");

            migrationBuilder.DropTable(
                name: "tbl_RequestClinics");

            migrationBuilder.DropTable(
                name: "tbl_Products");

            migrationBuilder.DropIndex(
                name: "IX_tbl_ApprovalFlows_PositionId",
                table: "tbl_ApprovalFlows");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserApproveId",
                table: "tbl_ApprovalFlows",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApprovalSettingType",
                table: "tbl_ApprovalFlows",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
