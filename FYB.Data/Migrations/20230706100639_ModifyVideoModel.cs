using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FYB.Data.Migrations
{
    public partial class ModifyVideoModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileExtension",
                table: "CoachingVideos",
                newName: "Path");

            migrationBuilder.AddColumn<string>(
                name: "ContentFileType",
                table: "CoachingVideos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentFileType",
                table: "CoachingVideos");

            migrationBuilder.RenameColumn(
                name: "Path",
                table: "CoachingVideos",
                newName: "FileExtension");
        }
    }
}
