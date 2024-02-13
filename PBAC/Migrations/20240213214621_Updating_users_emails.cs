using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PBAC.Migrations
{
    public partial class Updating_users_emails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("54bfc54e-386b-455f-b627-5b8800d7fc1a"),
                column: "Email",
                value: "moderator@gmail.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8db76dd5-c7ff-4550-b471-38d3f73446bb"),
                column: "Email",
                value: "user@gmail.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a8315f05-f401-4589-b976-61d129d3b1d7"),
                column: "Email",
                value: "admin@gmail.com");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("54bfc54e-386b-455f-b627-5b8800d7fc1a"),
                column: "Email",
                value: "someone@gmail.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8db76dd5-c7ff-4550-b471-38d3f73446bb"),
                column: "Email",
                value: "user.regular@gmail.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a8315f05-f401-4589-b976-61d129d3b1d7"),
                column: "Email",
                value: "maysam.m.mousa@gmail.com");
        }
    }
}
