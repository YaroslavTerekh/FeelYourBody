using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FYB.Data.Migrations
{
    public partial class AddNewDeleteBehaviorToFiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coaches_Files_AvatarId",
                table: "Coaches");

            migrationBuilder.DropForeignKey(
                name: "FK_Coachings_Files_CoachingPhotoId",
                table: "Coachings");

            migrationBuilder.DropIndex(
                name: "IX_Coachings_CoachingPhotoId",
                table: "Coachings");

            migrationBuilder.DropIndex(
                name: "IX_Coaches_AvatarId",
                table: "Coaches");

            migrationBuilder.CreateIndex(
                name: "IX_Files_CoachId",
                table: "Files",
                column: "CoachId",
                unique: true,
                filter: "[CoachId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Files_CoachingId",
                table: "Files",
                column: "CoachingId",
                unique: true,
                filter: "[CoachingId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Coaches_CoachId",
                table: "Files",
                column: "CoachId",
                principalTable: "Coaches",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Coachings_CoachingId",
                table: "Files",
                column: "CoachingId",
                principalTable: "Coachings",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Coaches_CoachId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Coachings_CoachingId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_CoachId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_CoachingId",
                table: "Files");

            migrationBuilder.CreateIndex(
                name: "IX_Coachings_CoachingPhotoId",
                table: "Coachings",
                column: "CoachingPhotoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Coaches_AvatarId",
                table: "Coaches",
                column: "AvatarId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Coaches_Files_AvatarId",
                table: "Coaches",
                column: "AvatarId",
                principalTable: "Files",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Coachings_Files_CoachingPhotoId",
                table: "Coachings",
                column: "CoachingPhotoId",
                principalTable: "Files",
                principalColumn: "Id");
        }
    }
}
