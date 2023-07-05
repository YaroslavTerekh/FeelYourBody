using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FYB.Data.Migrations
{
    public partial class ModifyFoodConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Food_Coachings_CoachingId",
                table: "Food");

            migrationBuilder.AddForeignKey(
                name: "FK_Food_Coachings_CoachingId",
                table: "Food",
                column: "CoachingId",
                principalTable: "Coachings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Food_Coachings_CoachingId",
                table: "Food");

            migrationBuilder.AddForeignKey(
                name: "FK_Food_Coachings_CoachingId",
                table: "Food",
                column: "CoachingId",
                principalTable: "Coachings",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
