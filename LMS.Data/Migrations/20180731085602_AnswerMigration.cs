using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Data.Migrations
{
    public partial class AnswerMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TaskType",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TaskType",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "TestSessionUser",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "TestId",
                table: "TestSessionUser",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TaskId = table.Column<int>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    Score = table.Column<double>(nullable: false),
                    TestSessionUserSessionId = table.Column<int>(nullable: true),
                    TestSessionUserUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Answers_TestSessionUser_TestSessionUserSessionId_TestSession~",
                        columns: x => new { x.TestSessionUserSessionId, x.TestSessionUserUserId },
                        principalTable: "TestSessionUser",
                        principalColumns: new[] { "SessionId", "UserId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestSessionUser_TestId",
                table: "TestSessionUser",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_TaskId",
                table: "Answers",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_TestSessionUserSessionId_TestSessionUserUserId",
                table: "Answers",
                columns: new[] { "TestSessionUserSessionId", "TestSessionUserUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TestSessionUser_Tests_TestId",
                table: "TestSessionUser",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestSessionUser_Tests_TestId",
                table: "TestSessionUser");

            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_TestSessionUser_TestId",
                table: "TestSessionUser");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "TestSessionUser");

            migrationBuilder.DropColumn(
                name: "TestId",
                table: "TestSessionUser");

            migrationBuilder.InsertData(
                table: "TaskType",
                columns: new[] { "Id", "Title" },
                values: new object[] { 3, "coding task" });

            migrationBuilder.InsertData(
                table: "TaskType",
                columns: new[] { "Id", "Title" },
                values: new object[] { 4, "modelling task" });
        }
    }
}
