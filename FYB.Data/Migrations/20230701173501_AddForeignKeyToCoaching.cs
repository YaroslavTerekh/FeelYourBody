using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FYB.Data.Migrations
{
    public partial class AddForeignKeyToCoaching : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Coachings_CoachingId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_CoachingId",
                table: "Files");

            migrationBuilder.CreateIndex(
                name: "IX_Coachings_CoachingPhotoId",
                table: "Coachings",
                column: "CoachingPhotoId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Coachings_Files_CoachingPhotoId",
                table: "Coachings",
                column: "CoachingPhotoId",
                principalTable: "Files",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coachings_Files_CoachingPhotoId",
                table: "Coachings");

            migrationBuilder.DropIndex(
                name: "IX_Coachings_CoachingPhotoId",
                table: "Coachings");

            migrationBuilder.CreateIndex(
                name: "IX_Files_CoachingId",
                table: "Files",
                column: "CoachingId",
                unique: true,
                filter: "[CoachingId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Coachings_CoachingId",
                table: "Files",
                column: "CoachingId",
                principalTable: "Coachings",
                principalColumn: "Id");
        }
    }
}
