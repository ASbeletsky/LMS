using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Data.Migrations
{
    public partial class addAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "5f9475a8-bc48-4280-b8af-2e60e0ca6366", "42b0446c-1818-416d-bf40-706292e044c4" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "d9991f24-1a85-4b39-8d89-7761c2f83ae9", "5d8410b6-8e6b-4ae6-9725-b35d0684d0e3" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "fcec3a90-f7b4-43e2-92a6-824f15a888a6", "31a008a9-43e2-48e1-94f6-8201d6e3e9d5" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "fd22c6aa-dd31-4a85-98d0-dd78768c59fc", "ab5379f1-b069-474f-b8ad-e1435f1b5754" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "f5615de1-c13a-4806-9acd-a3e66f8ad32d", "7ca3ebf5-79c8-453f-8f08-baea1251aaa5" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "fcec3a90-f7b4-43e2-92a6-824f15a888a6", "31a008a9-43e2-48e1-94f6-8201d6e3e9d5", "admin", "ADMIN" },
                    { "d9991f24-1a85-4b39-8d89-7761c2f83ae9", "5d8410b6-8e6b-4ae6-9725-b35d0684d0e3", "moderator", "MODERATOR" },
                    { "5f9475a8-bc48-4280-b8af-2e60e0ca6366", "42b0446c-1818-416d-bf40-706292e044c4", "reviewer", "REVIEWER" },
                    { "fd22c6aa-dd31-4a85-98d0-dd78768c59fc", "ab5379f1-b069-474f-b8ad-e1435f1b5754", "examinee", "EXAMINEE" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "f5615de1-c13a-4806-9acd-a3e66f8ad32d", 0, "7ca3ebf5-79c8-453f-8f08-baea1251aaa5", null, false, null, null, false, null, null, "ADMIN", "AQAAAAEAACcQAAAAEElnKplwSr93cNCB8fMF0CzSmEUOGq5aSGoEqZaSfRaMIDvTpAB7iPe7OuFWqfg1eA==", null, false, null, false, "admin" });
        }
    }
}
