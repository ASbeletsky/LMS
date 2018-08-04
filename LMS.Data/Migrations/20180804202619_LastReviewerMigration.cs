using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Data.Migrations
{
    public partial class LastReviewerMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastReviewDate",
                table: "TestSessionUser",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "LastReviewerId",
                table: "TestSessionUser",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestSessionUser_LastReviewerId",
                table: "TestSessionUser",
                column: "LastReviewerId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestSessionUser_AspNetUsers_LastReviewerId",
                table: "TestSessionUser",
                column: "LastReviewerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestSessionUser_AspNetUsers_LastReviewerId",
                table: "TestSessionUser");

            migrationBuilder.DropIndex(
                name: "IX_TestSessionUser_LastReviewerId",
                table: "TestSessionUser");

            migrationBuilder.DropColumn(
                name: "LastReviewDate",
                table: "TestSessionUser");

            migrationBuilder.DropColumn(
                name: "LastReviewerId",
                table: "TestSessionUser");
        }
    }
}
