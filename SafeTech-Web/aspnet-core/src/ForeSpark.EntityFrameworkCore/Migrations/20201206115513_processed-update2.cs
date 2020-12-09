using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ForeSpark.Migrations
{
    public partial class processedupdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Processed_AbpUsers_CreatorUserId",
                table: "Processed");

            migrationBuilder.DropForeignKey(
                name: "FK_Processed_AbpUsers_DeleterUserId",
                table: "Processed");

            migrationBuilder.DropForeignKey(
                name: "FK_Processed_AbpUsers_LastModifierUserId",
                table: "Processed");

            migrationBuilder.DropTable(
                name: "ProcessedMetadata");

            migrationBuilder.DropIndex(
                name: "IX_Processed_CreatorUserId",
                table: "Processed");

            migrationBuilder.DropIndex(
                name: "IX_Processed_DeleterUserId",
                table: "Processed");

            migrationBuilder.DropIndex(
                name: "IX_Processed_LastModifierUserId",
                table: "Processed");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "Processed");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Processed");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "Processed");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "Processed");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Processed");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "Processed");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "Processed");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Processed",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InVisionTime",
                table: "Processed",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "InstallationId",
                table: "Processed",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Processed_InstallationId",
                table: "Processed",
                column: "InstallationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Processed_Installations_InstallationId",
                table: "Processed",
                column: "InstallationId",
                principalTable: "Installations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Processed_Installations_InstallationId",
                table: "Processed");

            migrationBuilder.DropIndex(
                name: "IX_Processed_InstallationId",
                table: "Processed");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Processed");

            migrationBuilder.DropColumn(
                name: "InVisionTime",
                table: "Processed");

            migrationBuilder.DropColumn(
                name: "InstallationId",
                table: "Processed");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "Processed",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "Processed",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "Processed",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "Processed",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Processed",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "Processed",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "Processed",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProcessedMetadata",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InVisionTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InstallationId = table.Column<int>(type: "int", nullable: false),
                    ProcessedId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessedMetadata", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessedMetadata_Installations_InstallationId",
                        column: x => x.InstallationId,
                        principalTable: "Installations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_Processed_LastModifierUserId",
                table: "Processed",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessedMetadata_InstallationId",
                table: "ProcessedMetadata",
                column: "InstallationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessedMetadata_ProcessedId",
                table: "ProcessedMetadata",
                column: "ProcessedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Processed_AbpUsers_CreatorUserId",
                table: "Processed",
                column: "CreatorUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Processed_AbpUsers_DeleterUserId",
                table: "Processed",
                column: "DeleterUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Processed_AbpUsers_LastModifierUserId",
                table: "Processed",
                column: "LastModifierUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
