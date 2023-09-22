using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FYB.Data.Migrations
{
    public partial class ChangeCoachModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coaches_Files_AvatarId",
                table: "Coaches");

            migrationBuilder.DropIndex(
                name: "IX_Coaches_AvatarId",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "AvatarId",
                table: "Coaches");

            migrationBuilder.CreateTable(
                name: "CoachDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoachId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoachDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoachDetails_Coaches_CoachId",
                        column: x => x.CoachId,
                        principalTable: "Coaches",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Files_CoachId",
                table: "Files",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_CoachDetails_CoachId",
                table: "CoachDetails",
                column: "CoachId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Coaches_CoachId",
                table: "Files",
                column: "CoachId",
                principalTable: "Coaches",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Coaches_CoachId",
                table: "Files");

            migrationBuilder.DropTable(
                name: "CoachDetails");

            migrationBuilder.DropIndex(
                name: "IX_Files_CoachId",
                table: "Files");

            migrationBuilder.AddColumn<Guid>(
                name: "AvatarId",
                table: "Coaches",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
    }
}
