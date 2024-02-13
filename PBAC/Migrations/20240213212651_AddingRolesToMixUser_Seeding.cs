using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PBAC.Migrations
{
    public partial class AddingRolesToMixUser_Seeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UsersRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 2, new Guid("ffb2061b-815e-4f4a-a981-7846f76954ef") });

            migrationBuilder.InsertData(
                table: "UsersRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 3, new Guid("ffb2061b-815e-4f4a-a981-7846f76954ef") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UsersRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, new Guid("ffb2061b-815e-4f4a-a981-7846f76954ef") });

            migrationBuilder.DeleteData(
                table: "UsersRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 3, new Guid("ffb2061b-815e-4f4a-a981-7846f76954ef") });
        }
    }
}
