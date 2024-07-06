using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PersonalBlog.CategoryService.Api.Migrations
{
    /// <inheritdoc />
    public partial class init_db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "category");

            migrationBuilder.CreateTable(
                name: "CategoryVisibilityStatus",
                schema: "category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryVisibilityStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "category",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Archive_Categories")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "category")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "ToDate")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "FromDate"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Archive_Categories")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "category")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "ToDate")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "FromDate"),
                    Description = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Archive_Categories")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "category")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "ToDate")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "FromDate"),
                    CategoryVisibilityStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Archive_Categories")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "category")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "ToDate")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "FromDate"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Archive_Categories")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "category")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "ToDate")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "FromDate"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Archive_Categories")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "category")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "ToDate")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "FromDate"),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Archive_Categories")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "category")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "ToDate")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "FromDate"),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Archive_Categories")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "category")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "ToDate")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "FromDate"),
                    ConcurencyToken = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Archive_Categories")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "category")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "ToDate")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "FromDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_CategoryVisibilityStatus_CategoryVisibilityStatusId",
                        column: x => x.CategoryVisibilityStatusId,
                        principalSchema: "category",
                        principalTable: "CategoryVisibilityStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "Archive_Categories")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "category")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "ToDate")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "FromDate");

            migrationBuilder.InsertData(
                schema: "category",
                table: "CategoryVisibilityStatus",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "Enable" },
                    { 2, "Disable" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryVisibilityStatusId",
                schema: "category",
                table: "Categories",
                column: "CategoryVisibilityStatusId",
                unique: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories",
                schema: "category")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "Archive_Categories")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "category")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "ToDate")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "FromDate");

            migrationBuilder.DropTable(
                name: "CategoryVisibilityStatus",
                schema: "category");
        }
    }
}
