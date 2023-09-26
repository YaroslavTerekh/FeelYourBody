using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FYB.Data.Migrations
{
    public partial class ChangeCoachingConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoachingDetailParents_Coachings_CoachingId",
                table: "CoachingDetailParents");

            migrationBuilder.AddForeignKey(
                name: "FK_CoachingDetailParents_Coachings_CoachingId",
                table: "CoachingDetailParents",
                column: "CoachingId",
                principalTable: "Coachings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoachingDetailParents_Coachings_CoachingId",
                table: "CoachingDetailParents");

            migrationBuilder.AddForeignKey(
                name: "FK_CoachingDetailParents_Coachings_CoachingId",
                table: "CoachingDetailParents",
                column: "CoachingId",
                principalTable: "Coachings",
                principalColumn: "Id");
        }
    }
}
