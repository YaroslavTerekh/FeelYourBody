using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FYB.Data.Migrations
{
    public partial class AddDetailsToCoaching : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CoachingDetailsId",
                table: "Coachings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CoachingId",
                table: "CoachingDetails",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CoachingDetails_CoachingId",
                table: "CoachingDetails",
                column: "CoachingId");

            migrationBuilder.AddForeignKey(
                name: "FK_CoachingDetails_Coachings_CoachingId",
                table: "CoachingDetails",
                column: "CoachingId",
                principalTable: "Coachings",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoachingDetails_Coachings_CoachingId",
                table: "CoachingDetails");

            migrationBuilder.DropIndex(
                name: "IX_CoachingDetails_CoachingId",
                table: "CoachingDetails");

            migrationBuilder.DropColumn(
                name: "CoachingDetailsId",
                table: "Coachings");

            migrationBuilder.DropColumn(
                name: "CoachingId",
                table: "CoachingDetails");
        }
    }
}
