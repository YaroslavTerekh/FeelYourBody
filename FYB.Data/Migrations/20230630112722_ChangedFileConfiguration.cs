using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FYB.Data.Migrations
{
    public partial class ChangedFileConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coaches_Files_AvatarId",
                table: "Coaches");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Coachings_CoachingId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_CoachingId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Coaches_AvatarId",
                table: "Coaches");

            migrationBuilder.AlterColumn<Guid>(
                name: "CoachingId",
                table: "Files",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "CoachId",
                table: "Files",
                type: "uniqueidentifier",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "CoachId",
                table: "Files");

            migrationBuilder.AlterColumn<Guid>(
                name: "CoachingId",
                table: "Files",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Files_CoachingId",
                table: "Files",
                column: "CoachingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Coaches_AvatarId",
                table: "Coaches",
                column: "AvatarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coaches_Files_AvatarId",
                table: "Coaches",
                column: "AvatarId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Coachings_CoachingId",
                table: "Files",
                column: "CoachingId",
                principalTable: "Coachings",
                principalColumn: "Id");
        }
    }
}
