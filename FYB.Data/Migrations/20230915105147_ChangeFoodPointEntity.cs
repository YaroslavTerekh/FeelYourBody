using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FYB.Data.Migrations
{
    public partial class ChangeFoodPointEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "CoockingMethod",
                table: "FoodPoints",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "FoodPointParent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DayNumber = table.Column<int>(type: "int", nullable: false),
                    FoodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodPointParent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodPointParent_Food_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Food",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodPointParent_FoodId",
                table: "FoodPointParent",
                column: "FoodId");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodPoints_FoodPointParent_FoodPointsParentId",
                table: "FoodPoints",
                column: "FoodPointsParentId",
                principalTable: "FoodPointParent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodPoints_FoodPointParent_FoodPointsParentId",
                table: "FoodPoints");

            migrationBuilder.DropTable(
                name: "FoodPointParent");

            migrationBuilder.DropColumn(
                name: "CoockingMethod",
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
    }
}
