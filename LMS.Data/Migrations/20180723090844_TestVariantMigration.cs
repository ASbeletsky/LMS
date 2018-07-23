using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Data.Migrations
{
    public partial class TestVariantMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestVariant",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: false),
                    TestTemplateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestVariant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestVariant_TestTemplates_TestTemplateId",
                        column: x => x.TestTemplateId,
                        principalTable: "TestTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestVariantLevel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    TestVariantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestVariantLevel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestVariantLevel_TestVariant_TestVariantId",
                        column: x => x.TestVariantId,
                        principalTable: "TestVariant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestVariantLevelTask",
                columns: table => new
                {
                    LevelId = table.Column<int>(nullable: false),
                    TaskId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestVariantLevelTask", x => new { x.LevelId, x.TaskId });
                    table.ForeignKey(
                        name: "FK_TestVariantLevelTask_TestVariantLevel_LevelId",
                        column: x => x.LevelId,
                        principalTable: "TestVariantLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestVariantLevelTask_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestVariant_TestTemplateId",
                table: "TestVariant",
                column: "TestTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_TestVariantLevel_TestVariantId",
                table: "TestVariantLevel",
                column: "TestVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_TestVariantLevelTask_TaskId",
                table: "TestVariantLevelTask",
                column: "TaskId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestVariantLevelTask");

            migrationBuilder.DropTable(
                name: "TestVariantLevel");

            migrationBuilder.DropTable(
                name: "TestVariant");
        }
    }
}
