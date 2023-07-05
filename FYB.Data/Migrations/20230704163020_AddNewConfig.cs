using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FYB.Data.Migrations
{
    public partial class AddNewConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoachingDetails_Coachings_CoachingId",
                table: "CoachingDetails");

            migrationBuilder.AlterColumn<Guid>(
                name: "CoachingId",
                table: "CoachingDetails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CoachingDetails_Coachings_CoachingId",
                table: "CoachingDetails",
                column: "CoachingId",
                principalTable: "Coachings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoachingDetails_Coachings_CoachingId",
                table: "CoachingDetails");

            migrationBuilder.AlterColumn<Guid>(
                name: "CoachingId",
                table: "CoachingDetails",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_CoachingDetails_Coachings_CoachingId",
                table: "CoachingDetails",
                column: "CoachingId",
                principalTable: "Coachings",
                principalColumn: "Id");
        }
    }
}
