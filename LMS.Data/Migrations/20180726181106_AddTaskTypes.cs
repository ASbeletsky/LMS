using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Data.Migrations
{
    public partial class AddTaskTypes : Migration
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

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskType",
                table: "TaskType",
                column: "Id");

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

            migrationBuilder.InsertData(
               table: "TaskType",
               columns: new[] { "Id", "Title" },
               values: new object[] { 3, "Coding task" });

            migrationBuilder.InsertData(
                table: "TaskType",
                columns: new[] { "Id", "Title" },
                values: new object[] { 4, "Modelling task" });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LevelTaskType_TaskType_TaskTypeId",
                table: "LevelTaskType");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_TaskType_TypeId",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "TaskAnswerOption");

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

            migrationBuilder.DeleteData(
               table: "TaskType",
               keyColumn: "Id",
               keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TaskType",
                keyColumn: "Id",
                keyValue: 4);

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
