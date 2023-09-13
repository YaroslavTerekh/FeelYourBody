using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FYB.Data.Migrations
{
    public partial class ChangeCoachingDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoachingDetails_Coachings_CoachingId",
                table: "CoachingDetails");

            migrationBuilder.RenameColumn(
                name: "CoachingId",
                table: "CoachingDetails",
                newName: "CoachingDetailsParentId");

            migrationBuilder.RenameIndex(
                name: "IX_CoachingDetails_CoachingId",
                table: "CoachingDetails",
                newName: "IX_CoachingDetails_CoachingDetailsParentId");

            migrationBuilder.CreateTable(
                name: "CoachingDetailParents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoachingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoachingDetailParents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoachingDetailParents_Coachings_CoachingId",
                        column: x => x.CoachingId,
                        principalTable: "Coachings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoachingDetailParents_CoachingId",
                table: "CoachingDetailParents",
                column: "CoachingId");

            migrationBuilder.AddForeignKey(
                name: "FK_CoachingDetails_CoachingDetailParents_CoachingDetailsParentId",
                table: "CoachingDetails",
                column: "CoachingDetailsParentId",
                principalTable: "CoachingDetailParents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoachingDetails_CoachingDetailParents_CoachingDetailsParentId",
                table: "CoachingDetails");

            migrationBuilder.DropTable(
                name: "CoachingDetailParents");

            migrationBuilder.RenameColumn(
                name: "CoachingDetailsParentId",
                table: "CoachingDetails",
                newName: "CoachingId");

            migrationBuilder.RenameIndex(
                name: "IX_CoachingDetails_CoachingDetailsParentId",
                table: "CoachingDetails",
                newName: "IX_CoachingDetails_CoachingId");

            migrationBuilder.AddForeignKey(
                name: "FK_CoachingDetails_Coachings_CoachingId",
                table: "CoachingDetails",
                column: "CoachingId",
                principalTable: "Coachings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
