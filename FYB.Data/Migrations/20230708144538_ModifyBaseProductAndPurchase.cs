using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FYB.Data.Migrations
{
    public partial class ModifyBaseProductAndPurchase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoachingPurchases_AspNetUsers_UserId",
                table: "CoachingPurchases");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodPurchases_AspNetUsers_UserId",
                table: "FoodPurchases");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "FoodPurchases",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UnixExpireTime",
                table: "Food",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UnixExpireTime",
                table: "Coachings",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "CoachingPurchases",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CoachingPurchases_AspNetUsers_UserId",
                table: "CoachingPurchases",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodPurchases_AspNetUsers_UserId",
                table: "FoodPurchases",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoachingPurchases_AspNetUsers_UserId",
                table: "CoachingPurchases");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodPurchases_AspNetUsers_UserId",
                table: "FoodPurchases");

            migrationBuilder.DropColumn(
                name: "UnixExpireTime",
                table: "Food");

            migrationBuilder.DropColumn(
                name: "UnixExpireTime",
                table: "Coachings");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "FoodPurchases",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "CoachingPurchases",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_CoachingPurchases_AspNetUsers_UserId",
                table: "CoachingPurchases",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodPurchases_AspNetUsers_UserId",
                table: "FoodPurchases",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
