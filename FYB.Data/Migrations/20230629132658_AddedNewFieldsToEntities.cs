using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FYB.Data.Migrations
{
    public partial class AddedNewFieldsToEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CoachingId",
                table: "Files",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "InstagramLink",
                table: "Feedbacks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "CoachingPhotoId",
                table: "Coachings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Files_CoachingId",
                table: "Files",
                column: "CoachingId",
                unique: true);

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
                name: "FK_Files_Coachings_CoachingId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_CoachingId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "CoachingId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "InstagramLink",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "CoachingPhotoId",
                table: "Coachings");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "AspNetUsers");
        }
    }
}
