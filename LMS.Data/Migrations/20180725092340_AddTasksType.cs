using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Data.Migrations
{
    public partial class AddTasksType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                table: "TaskTypes",
                columns: new[] { "Id", "Title" },
                values: new object[] { 1, "open-ended question" });

            migrationBuilder.InsertData(
                table: "TaskTypes",
                columns: new[] { "Id", "Title" },
                values: new object[] { 2, "question with options" });

            migrationBuilder.CreateIndex(
                name: "IX_TaskAnswerOption_TaskId",
                table: "TaskAnswerOption",
                column: "TaskId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskAnswerOption");

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
