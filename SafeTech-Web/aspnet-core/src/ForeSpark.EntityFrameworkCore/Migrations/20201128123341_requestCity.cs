using Microsoft.EntityFrameworkCore.Migrations;

namespace ForeSpark.Migrations
{
    public partial class requestCity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Requests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_CityId",
                table: "Requests",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Cities_CityId",
                table: "Requests",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Cities_CityId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_CityId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Requests");
        }
    }
}
