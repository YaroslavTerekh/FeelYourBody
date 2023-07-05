using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FYB.Data.Migrations
{
    public partial class AddFoodPointConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodPoints_Food_FoodId",
                table: "FoodPoints");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodPoints_Food_FoodId",
                table: "FoodPoints",
                column: "FoodId",
                principalTable: "Food",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodPoints_Food_FoodId",
                table: "FoodPoints");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodPoints_Food_FoodId",
                table: "FoodPoints",
                column: "FoodId",
                principalTable: "Food",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
