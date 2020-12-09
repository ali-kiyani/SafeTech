using Microsoft.EntityFrameworkCore.Migrations;

namespace ForeSpark.Migrations
{
    public partial class processedupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Processed_Installations_InstallationId",
                table: "Processed");

            migrationBuilder.DropIndex(
                name: "IX_Processed_InstallationId",
                table: "Processed");

            migrationBuilder.DropColumn(
                name: "InstallationId",
                table: "Processed");

            migrationBuilder.AddColumn<int>(
                name: "InstallationId",
                table: "ProcessedMetadata",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProcessedMetadata_InstallationId",
                table: "ProcessedMetadata",
                column: "InstallationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessedMetadata_Installations_InstallationId",
                table: "ProcessedMetadata",
                column: "InstallationId",
                principalTable: "Installations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcessedMetadata_Installations_InstallationId",
                table: "ProcessedMetadata");

            migrationBuilder.DropIndex(
                name: "IX_ProcessedMetadata_InstallationId",
                table: "ProcessedMetadata");

            migrationBuilder.DropColumn(
                name: "InstallationId",
                table: "ProcessedMetadata");

            migrationBuilder.AddColumn<int>(
                name: "InstallationId",
                table: "Processed",
                type: "int",
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
    }
}
