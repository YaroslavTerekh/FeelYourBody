using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FYB.Data.Migrations
{
    public partial class ChangeFoodPointParentEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodPointParent_Food_FoodId",
                table: "FoodPointParent");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodPoints_FoodPointParent_FoodPointsParentId",
                table: "FoodPoints");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FoodPointParent",
                table: "FoodPointParent");

            migrationBuilder.RenameTable(
                name: "FoodPointParent",
                newName: "FoodPointParents");

            migrationBuilder.RenameIndex(
                name: "IX_FoodPointParent_FoodId",
                table: "FoodPointParents",
                newName: "IX_FoodPointParents_FoodId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FoodPointParents",
                table: "FoodPointParents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodPointParents_Food_FoodId",
                table: "FoodPointParents",
                column: "FoodId",
                principalTable: "Food",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodPoints_FoodPointParents_FoodPointsParentId",
                table: "FoodPoints",
                column: "FoodPointsParentId",
                principalTable: "FoodPointParents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodPointParents_Food_FoodId",
                table: "FoodPointParents");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodPoints_FoodPointParents_FoodPointsParentId",
                table: "FoodPoints");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FoodPointParents",
                table: "FoodPointParents");

            migrationBuilder.RenameTable(
                name: "FoodPointParents",
                newName: "FoodPointParent");

            migrationBuilder.RenameIndex(
                name: "IX_FoodPointParents_FoodId",
                table: "FoodPointParent",
                newName: "IX_FoodPointParent_FoodId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FoodPointParent",
                table: "FoodPointParent",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodPointParent_Food_FoodId",
                table: "FoodPointParent",
                column: "FoodId",
                principalTable: "Food",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodPoints_FoodPointParent_FoodPointsParentId",
                table: "FoodPoints",
                column: "FoodPointsParentId",
                principalTable: "FoodPointParent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
