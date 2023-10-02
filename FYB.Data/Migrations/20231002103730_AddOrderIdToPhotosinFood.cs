using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FYB.Data.Migrations
{
    public partial class AddOrderIdToPhotosinFood : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Food_FoodAvatars_AvatarId",
                table: "Food");

            migrationBuilder.DropIndex(
                name: "IX_Food_AvatarId",
                table: "Food");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "FoodPhotos",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "FoodPhotos");

            migrationBuilder.CreateIndex(
                name: "IX_Food_AvatarId",
                table: "Food",
                column: "AvatarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Food_FoodAvatars_AvatarId",
                table: "Food",
                column: "AvatarId",
                principalTable: "FoodAvatars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
