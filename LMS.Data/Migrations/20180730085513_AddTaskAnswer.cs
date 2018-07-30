using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Data.Migrations
{
    public partial class AddTaskAnswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LevelTaskType_TaskTypes_TaskTypeId",
                table: "LevelTaskType");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_TaskTypes_TypeId",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskTypes",
                table: "TaskTypes");

            migrationBuilder.RenameTable(
                name: "TaskTypes",
                newName: "TaskType");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "TestSessionUser",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "TestId",
                table: "TestSessionUser",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskType",
                table: "TaskType",
                column: "Id");

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
                    TestSessionUserUserId = table.Column<string>(nullable: true),
                    TestSessionUserId = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "TaskAnswerOption",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TaskId = table.Column<int>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    IsCorrect = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskAnswerOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskAnswerOption_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TaskType",
                columns: new[] { "Id", "Title" },
                values: new object[] { 1, "open-ended question" });

            migrationBuilder.InsertData(
                table: "TaskType",
                columns: new[] { "Id", "Title" },
                values: new object[] { 2, "question with options" });

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

            migrationBuilder.CreateIndex(
                name: "IX_TaskAnswerOption_TaskId",
                table: "TaskAnswerOption",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_LevelTaskType_TaskType_TaskTypeId",
                table: "LevelTaskType",
                column: "TaskTypeId",
                principalTable: "TaskType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_TaskType_TypeId",
                table: "Tasks",
                column: "TypeId",
                principalTable: "TaskType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_LevelTaskType_TaskType_TaskTypeId",
                table: "LevelTaskType");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_TaskType_TypeId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_TestSessionUser_Tests_TestId",
                table: "TestSessionUser");

            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "TaskAnswerOption");

            migrationBuilder.DropIndex(
                name: "IX_TestSessionUser_TestId",
                table: "TestSessionUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskType",
                table: "TaskType");

            migrationBuilder.DeleteData(
                table: "TaskType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TaskType",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "TestSessionUser");

            migrationBuilder.DropColumn(
                name: "TestId",
                table: "TestSessionUser");

            migrationBuilder.RenameTable(
                name: "TaskType",
                newName: "TaskTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskTypes",
                table: "TaskTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LevelTaskType_TaskTypes_TaskTypeId",
                table: "LevelTaskType",
                column: "TaskTypeId",
                principalTable: "TaskTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_TaskTypes_TypeId",
                table: "Tasks",
                column: "TypeId",
                principalTable: "TaskTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
