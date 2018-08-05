using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Data.Migrations
{
    public partial class TimesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_TestSessionUser_TestSessionUserSessionId_TestSession~",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_TestSessionUser_AspNetUsers_LastReviewerId",
                table: "TestSessionUser");

            migrationBuilder.DropForeignKey(
                name: "FK_TestSessionUser_TestSessions_SessionId",
                table: "TestSessionUser");

            migrationBuilder.DropForeignKey(
                name: "FK_TestSessionUser_Tests_TestId",
                table: "TestSessionUser");

            migrationBuilder.DropForeignKey(
                name: "FK_TestSessionUser_AspNetUsers_UserId",
                table: "TestSessionUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestSessionUser",
                table: "TestSessionUser");

            migrationBuilder.RenameTable(
                name: "TestSessionUser",
                newName: "TestSessionUsers");

            migrationBuilder.RenameIndex(
                name: "IX_TestSessionUser_UserId",
                table: "TestSessionUsers",
                newName: "IX_TestSessionUsers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TestSessionUser_TestId",
                table: "TestSessionUsers",
                newName: "IX_TestSessionUsers_TestId");

            migrationBuilder.RenameIndex(
                name: "IX_TestSessionUser_LastReviewerId",
                table: "TestSessionUsers",
                newName: "IX_TestSessionUsers_LastReviewerId");

            migrationBuilder.AddColumn<bool>(
                name: "Baned",
                table: "TestSessionUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "TestSessionUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "EndTime",
                table: "TestSessionUsers",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "StartTime",
                table: "TestSessionUsers",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddUniqueConstraint(
                name: "AK_TestSessionUsers_Code",
                table: "TestSessionUsers",
                column: "Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestSessionUsers",
                table: "TestSessionUsers",
                columns: new[] { "SessionId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_TestSessionUsers_TestSessionUserSessionId_TestSessio~",
                table: "Answers",
                columns: new[] { "TestSessionUserSessionId", "TestSessionUserUserId" },
                principalTable: "TestSessionUsers",
                principalColumns: new[] { "SessionId", "UserId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestSessionUsers_AspNetUsers_LastReviewerId",
                table: "TestSessionUsers",
                column: "LastReviewerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestSessionUsers_TestSessions_SessionId",
                table: "TestSessionUsers",
                column: "SessionId",
                principalTable: "TestSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestSessionUsers_Tests_TestId",
                table: "TestSessionUsers",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_TestSessionUsers_AspNetUsers_UserId",
                table: "TestSessionUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_TestSessionUsers_TestSessionUserSessionId_TestSessio~",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_TestSessionUsers_AspNetUsers_LastReviewerId",
                table: "TestSessionUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_TestSessionUsers_TestSessions_SessionId",
                table: "TestSessionUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_TestSessionUsers_Tests_TestId",
                table: "TestSessionUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_TestSessionUsers_AspNetUsers_UserId",
                table: "TestSessionUsers");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_TestSessionUsers_Code",
                table: "TestSessionUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestSessionUsers",
                table: "TestSessionUsers");

            migrationBuilder.DropColumn(
                name: "Baned",
                table: "TestSessionUsers");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "TestSessionUsers");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "TestSessionUsers");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "TestSessionUsers");

            migrationBuilder.RenameTable(
                name: "TestSessionUsers",
                newName: "TestSessionUser");

            migrationBuilder.RenameIndex(
                name: "IX_TestSessionUsers_UserId",
                table: "TestSessionUser",
                newName: "IX_TestSessionUser_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TestSessionUsers_TestId",
                table: "TestSessionUser",
                newName: "IX_TestSessionUser_TestId");

            migrationBuilder.RenameIndex(
                name: "IX_TestSessionUsers_LastReviewerId",
                table: "TestSessionUser",
                newName: "IX_TestSessionUser_LastReviewerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestSessionUser",
                table: "TestSessionUser",
                columns: new[] { "SessionId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_TestSessionUser_TestSessionUserSessionId_TestSession~",
                table: "Answers",
                columns: new[] { "TestSessionUserSessionId", "TestSessionUserUserId" },
                principalTable: "TestSessionUser",
                principalColumns: new[] { "SessionId", "UserId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestSessionUser_AspNetUsers_LastReviewerId",
                table: "TestSessionUser",
                column: "LastReviewerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestSessionUser_TestSessions_SessionId",
                table: "TestSessionUser",
                column: "SessionId",
                principalTable: "TestSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestSessionUser_Tests_TestId",
                table: "TestSessionUser",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_TestSessionUser_AspNetUsers_UserId",
                table: "TestSessionUser",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
