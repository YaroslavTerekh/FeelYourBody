using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FYB.Data.Migrations
{
    public partial class AddPhotosToChoaching : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Feedbacks_FeedbackId",
                table: "Files");

            migrationBuilder.RenameColumn(
                name: "FeedbackId",
                table: "Files",
                newName: "FeedBackId");

            migrationBuilder.RenameIndex(
                name: "IX_Files_FeedbackId",
                table: "Files",
                newName: "IX_Files_FeedBackId");

            migrationBuilder.AddColumn<Guid>(
                name: "CoachingListId",
                table: "Files",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Files_CoachingListId",
                table: "Files",
                column: "CoachingListId");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Files_CoachId_Or_FeedBackId_Or_CoachingListId_Or_CoachingId",
                table: "Files",
                sql: "CASE WHEN CoachId IS NOT NULL THEN (CASE WHEN FeedBackId IS NULL AND CoachingListId IS NULL AND CoachingId IS NULL THEN 1 ELSE 0 END) WHEN FeedBackId IS NOT NULL THEN (CASE WHEN CoachingListId IS NULL AND CoachingId IS NULL THEN 1 ELSE 0 END) WHEN CoachingListId IS NOT NULL THEN (CASE WHEN CoachingId IS NULL THEN 1 ELSE 0 END) ELSE 1 END = 1");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Coachings_CoachingListId",
                table: "Files",
                column: "CoachingListId",
                principalTable: "Coachings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Feedbacks_FeedBackId",
                table: "Files",
                column: "FeedBackId",
                principalTable: "Feedbacks",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Coachings_CoachingListId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Feedbacks_FeedBackId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_CoachingListId",
                table: "Files");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Files_CoachId_Or_FeedBackId_Or_CoachingListId_Or_CoachingId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "CoachingListId",
                table: "Files");

            migrationBuilder.RenameColumn(
                name: "FeedBackId",
                table: "Files",
                newName: "FeedbackId");

            migrationBuilder.RenameIndex(
                name: "IX_Files_FeedBackId",
                table: "Files",
                newName: "IX_Files_FeedbackId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Feedbacks_FeedbackId",
                table: "Files",
                column: "FeedbackId",
                principalTable: "Feedbacks",
                principalColumn: "Id");
        }
    }
}
