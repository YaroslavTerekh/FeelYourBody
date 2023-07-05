using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FYB.Data.Migrations
{
    public partial class ModifyCoachingConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coachings_Food_FoodId",
                table: "Coachings");

            migrationBuilder.DropIndex(
                name: "IX_Coachings_FoodId",
                table: "Coachings");

            migrationBuilder.DropColumn(
                name: "CoachingDetailsId",
                table: "Coachings");

            migrationBuilder.CreateIndex(
                name: "IX_Food_CoachingId",
                table: "Food",
                column: "CoachingId",
                unique: true,
                filter: "[CoachingId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Food_Coachings_CoachingId",
                table: "Food",
                column: "CoachingId",
                principalTable: "Coachings",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Food_Coachings_CoachingId",
                table: "Food");

            migrationBuilder.DropIndex(
                name: "IX_Food_CoachingId",
                table: "Food");

            migrationBuilder.AddColumn<Guid>(
                name: "CoachingDetailsId",
                table: "Coachings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Coachings_FoodId",
                table: "Coachings",
                column: "FoodId",
                unique: true,
                filter: "[FoodId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Coachings_Food_FoodId",
                table: "Coachings",
                column: "FoodId",
                principalTable: "Food",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
