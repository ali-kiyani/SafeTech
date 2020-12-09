using Microsoft.EntityFrameworkCore.Migrations;

namespace ForeSpark.Migrations
{
    public partial class productImagesupdatenew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductImages_ProductImagesId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductImagesId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductImagesId",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductImagesId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductImagesId",
                table: "Products",
                column: "ProductImagesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductImages_ProductImagesId",
                table: "Products",
                column: "ProductImagesId",
                principalTable: "ProductImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
