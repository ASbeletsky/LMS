using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Data.Migrations
{
    public partial class TestTemplateMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: false),
                    Duration = table.Column<TimeSpan>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestTemplateLevel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Count = table.Column<int>(nullable: false),
                    MaxScore = table.Column<double>(nullable: false),
                    MinComplexity = table.Column<int>(nullable: false),
                    MaxComplexity = table.Column<int>(nullable: false),
                    TestTemplateLevelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestTemplateLevel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestTemplateLevel_TestTemplates_TestTemplateLevelId",
                        column: x => x.TestTemplateLevelId,
                        principalTable: "TestTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LevelCategory",
                columns: table => new
                {
                    TestTemplateLevelId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LevelCategory", x => new { x.CategoryId, x.TestTemplateLevelId });
                    table.ForeignKey(
                        name: "FK_LevelCategory_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LevelCategory_TestTemplateLevel_TestTemplateLevelId",
                        column: x => x.TestTemplateLevelId,
                        principalTable: "TestTemplateLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LevelTaskType",
                columns: table => new
                {
                    TestTemplateLevelId = table.Column<int>(nullable: false),
                    TaskTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LevelTaskType", x => new { x.TaskTypeId, x.TestTemplateLevelId });
                    table.ForeignKey(
                        name: "FK_LevelTaskType_TaskTypes_TaskTypeId",
                        column: x => x.TaskTypeId,
                        principalTable: "TaskTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LevelTaskType_TestTemplateLevel_TestTemplateLevelId",
                        column: x => x.TestTemplateLevelId,
                        principalTable: "TestTemplateLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LevelCategory_TestTemplateLevelId",
                table: "LevelCategory",
                column: "TestTemplateLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_LevelTaskType_TestTemplateLevelId",
                table: "LevelTaskType",
                column: "TestTemplateLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_TestTemplateLevel_TestTemplateLevelId",
                table: "TestTemplateLevel",
                column: "TestTemplateLevelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LevelCategory");

            migrationBuilder.DropTable(
                name: "LevelTaskType");

            migrationBuilder.DropTable(
                name: "TestTemplateLevel");

            migrationBuilder.DropTable(
                name: "TestTemplates");
        }
    }
}
