using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ForeSpark.Migrations
{
    public partial class processed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Processed",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<int>(nullable: false),
                    InstallationId = table.Column<int>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processed", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Processed_AbpUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Processed_AbpUsers_DeleterUserId",
                        column: x => x.DeleterUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Processed_Installations_InstallationId",
                        column: x => x.InstallationId,
                        principalTable: "Installations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Processed_AbpUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Processed_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProcessedMetadata",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessedId = table.Column<int>(nullable: false),
                    InVisionTime = table.Column<DateTime>(nullable: false),
                    FileName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessedMetadata", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessedMetadata_Processed_ProcessedId",
                        column: x => x.ProcessedId,
                        principalTable: "Processed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Processed_CreatorUserId",
                table: "Processed",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Processed_DeleterUserId",
                table: "Processed",
                column: "DeleterUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Processed_InstallationId",
                table: "Processed",
                column: "InstallationId");

            migrationBuilder.CreateIndex(
                name: "IX_Processed_LastModifierUserId",
                table: "Processed",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Processed_RequestId",
                table: "Processed",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessedMetadata_ProcessedId",
                table: "ProcessedMetadata",
                column: "ProcessedId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcessedMetadata");

            migrationBuilder.DropTable(
                name: "Processed");
        }
    }
}
