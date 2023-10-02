using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FYB.Data.Migrations
{
    public partial class AddPhotosToFood : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "FoodId",
                table: "FoodDetail",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AvatarId",
                table: "Food",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "FoodAvatars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodAvatars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FoodPhotos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FoodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodPhotos_Food_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Food",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Food_AvatarId",
                table: "Food",
                column: "AvatarId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodPhotos_FoodId",
                table: "FoodPhotos",
                column: "FoodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Food_FoodAvatars_AvatarId",
                table: "Food",
                column: "AvatarId",
                principalTable: "FoodAvatars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Food_FoodAvatars_AvatarId",
                table: "Food");

            migrationBuilder.DropTable(
                name: "FoodAvatars");

            migrationBuilder.DropTable(
                name: "FoodPhotos");

            migrationBuilder.DropIndex(
                name: "IX_Food_AvatarId",
                table: "Food");

            migrationBuilder.DropColumn(
                name: "AvatarId",
                table: "Food");

            migrationBuilder.AlterColumn<Guid>(
                name: "FoodId",
                table: "FoodDetail",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}
