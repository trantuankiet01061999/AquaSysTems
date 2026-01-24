using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AquaSolution.Data.Migrations
{
    public partial class Update_table_KPI_task : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Drop FK OwnerId nếu tồn tại
            migrationBuilder.Sql(@"
                IF EXISTS (
                    SELECT 1
                    FROM sys.foreign_keys
                    WHERE name = 'FK_tbl_KPITasks_tbl_Users_OwnerId'
                )
                BEGIN
                    ALTER TABLE [KPI].[tbl_KPITasks]
                    DROP CONSTRAINT [FK_tbl_KPITasks_tbl_Users_OwnerId]
                END
            ");

            // 2. Drop index OwnerId nếu tồn tại
            migrationBuilder.Sql(@"
                IF EXISTS (
                    SELECT 1
                    FROM sys.indexes
                    WHERE name = 'IX_tbl_KPITasks_OwnerId'
                      AND object_id = OBJECT_ID('[KPI].[tbl_KPITasks]')
                )
                BEGIN
                    DROP INDEX IX_tbl_KPITasks_OwnerId 
                    ON [KPI].[tbl_KPITasks]
                END
            ");

            // 3. Drop column OwnerId nếu tồn tại
            migrationBuilder.Sql(@"
                IF EXISTS (
                    SELECT 1
                    FROM sys.columns
                    WHERE name = 'OwnerId'
                      AND object_id = OBJECT_ID('[KPI].[tbl_KPITasks]')
                )
                BEGIN
                    ALTER TABLE [KPI].[tbl_KPITasks]
                    DROP COLUMN [OwnerId]
                END
            ");

            // 4. Add PIC column nếu chưa có
            migrationBuilder.Sql(@"
                IF NOT EXISTS (
                    SELECT 1
                    FROM sys.columns
                    WHERE name = 'PIC'
                      AND object_id = OBJECT_ID('[KPI].[tbl_KPITasks]')
                )
                BEGIN
                    ALTER TABLE [KPI].[tbl_KPITasks]
                    ADD [PIC] NVARCHAR(MAX) NOT NULL DEFAULT ''
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // ❌ Không rollback để tránh phá dữ liệu production
        }
    }
}
