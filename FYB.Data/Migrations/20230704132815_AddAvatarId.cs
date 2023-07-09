using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FYB.Data.Migrations
{
    public partial class AddAvatarId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Coaches_CoachId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_CoachId",
                table: "Files");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coaches_Files_AvatarId",
                table: "Coaches");

            migrationBuilder.DropIndex(
                name: "IX_Coaches_AvatarId",
                table: "Coaches");

            migrationBuilder.CreateIndex(
                name: "IX_Files_CoachId",
                table: "Files",
                column: "CoachId",
                unique: true,
                filter: "[CoachId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Coaches_CoachId",
                table: "Files",
                column: "CoachId",
                principalTable: "Coaches",
                principalColumn: "Id");
        }
    }
}
