using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FYB.Data.Migrations
{
    public partial class deleteFoodPointParent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodPoints_FoodPointParents_FoodPointsParentId",
                table: "FoodPoints");

            migrationBuilder.RenameColumn(
                name: "FoodPointsParentId",
                table: "FoodPoints",
                newName: "FoodId");

            migrationBuilder.RenameIndex(
                name: "IX_FoodPoints_FoodPointsParentId",
                table: "FoodPoints",
                newName: "IX_FoodPoints_FoodId");

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

            migrationBuilder.RenameColumn(
                name: "FoodId",
                table: "FoodPoints",
                newName: "FoodPointsParentId");

            migrationBuilder.RenameIndex(
                name: "IX_FoodPoints_FoodId",
                table: "FoodPoints",
                newName: "IX_FoodPoints_FoodPointsParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodPoints_FoodPointParents_FoodPointsParentId",
                table: "FoodPoints",
                column: "FoodPointsParentId",
                principalTable: "FoodPointParents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
