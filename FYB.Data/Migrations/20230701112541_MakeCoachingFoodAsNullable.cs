using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FYB.Data.Migrations
{
    public partial class MakeCoachingFoodAsNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Coachings_FoodId",
                table: "Coachings");

            migrationBuilder.AlterColumn<Guid>(
                name: "FoodId",
                table: "Coachings",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Coachings_FoodId",
                table: "Coachings",
                column: "FoodId",
                unique: true,
                filter: "[FoodId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Coachings_FoodId",
                table: "Coachings");

            migrationBuilder.AlterColumn<Guid>(
                name: "FoodId",
                table: "Coachings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Coachings_FoodId",
                table: "Coachings",
                column: "FoodId",
                unique: true);
        }
    }
}
